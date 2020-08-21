﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

// 추가한 것들.
using System.Windows.Threading;
using System.Threading;
using System.IO;
using System.Media;
using System.Timers;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Kinect;


//키넥트 버전 투우우!!!

namespace WpfApplication4
{


    public partial class SubWindow : Window
    {
        private KinectSensor kinectSensor = null; // 키넥트 센서 정의

        private FrameDescription depthFrameDescription = null;

        // Define Brushes for drawing skeletons
        private const double HandSize = 30;
        private const double JointThickness = 5;
        private const float InferredZPositionClamp = 0.1f;

        private readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
        private readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));
        private readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private readonly Brush inferredJointBrush = Brushes.Yellow;
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        private DrawingGroup drawingGroup;
        private DrawingImage SkeletonimageSource;

        // Skeleton Video Setup
        private CoordinateMapper coordinateMapper = null;
        private BodyFrameReader bodyFrameReader = null;
        private Body[] bodies = null;
        private List<Tuple<JointType, JointType>> bones;

        private int displayWidth;
        private int displayHeight;

        private List<Pen> bodyColors;

        // 10초 타이머 설정을 위한 초기 변수 정의
        DispatcherTimer _timer;
        TimeSpan _time;

        // 일시정지, 게임오버, 실패여부, 타이머작동여부 초기설정
        private bool isPause = true;
        private bool isFall = false;
        private int isTimerOn = 0;
        private bool texthelper = true;

        private int trialNo = 0;
        private double[] timeLeft = new double[3], timeRight = new double[3], bsLeft = new double[3], bsRight = new double[3];

        private double balanceScoreLeft = 100, balanceScoreRight = 100;
        public static double bestTimeLeft = 0, bestTimeRight = 0, finalbsLeft = 0, finalbsRight = 0;

        List<CameraSpacePoint> gravities = new List<CameraSpacePoint>();


        // 효과음 및 배경음 설정
        MediaPlayer TimesPlayer = new MediaPlayer();
        MediaPlayer gameoverPlayer = new MediaPlayer();
        MediaPlayer gamecompletePlayer = new MediaPlayer();
        MediaPlayer gamemusicPlayer = new MediaPlayer();
        MediaPlayer fallPlayer = new MediaPlayer();
        MediaPlayer ringPlayer = new MediaPlayer();

        string currentPath = System.IO.Directory.GetCurrentDirectory();

        public SubWindow()
        {
            // Get Kinect sensor object
            this.kinectSensor = KinectSensor.GetDefault();

            // Skeleton frames
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;
            this.depthFrameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            this.displayWidth = depthFrameDescription.Width;
            this.displayHeight = depthFrameDescription.Height;
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // Bone defined as a line between two joints
            this.bones = new List<Tuple<JointType, JointType>>();

            // Torso
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

            // Right Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight));

            // Left Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft));

            // Right Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

            // Left Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));

            // Different body colors for each BodyIndex
            this.bodyColors = new List<Pen>();
            this.bodyColors.Add(new Pen(Brushes.Black, 6));
            this.bodyColors.Add(new Pen(Brushes.Black, 6));
            this.bodyColors.Add(new Pen(Brushes.Black, 6));
            this.bodyColors.Add(new Pen(Brushes.Black, 6));
            this.bodyColors.Add(new Pen(Brushes.Black, 6));
            this.bodyColors.Add(new Pen(Brushes.Black, 6));

            // Open the sensor and initialize
            this.kinectSensor.Open();

            this.drawingGroup = new DrawingGroup();
            this.SkeletonimageSource = new DrawingImage(this.drawingGroup);

            this.DataContext = this;

            InitializeComponent();

            // 배경 이미지 불러오기
            System.Drawing.Bitmap img = WpfApplication4.Properties.Resources.Interface;
            MemoryStream imgStream = new MemoryStream();
            img.Save(imgStream, System.Drawing.Imaging.ImageFormat.Bmp);
            imgStream.Seek(0, SeekOrigin.Begin);
            BitmapFrame newimg = BitmapFrame.Create(imgStream);
            BackgroundImage.Source = newimg;

            // 각종 효과음 파일 불러오기 (경로를 재설정해주지 않으면 오류 발생)
            TimesPlayer.Open(new Uri(currentPath + "/Resources/beep-07.wav"));
            gameoverPlayer.Open(new Uri(currentPath + "/Resources/gameover.wav"));
            gamecompletePlayer.Open(new Uri(currentPath + "/Resources/applause-3.wav"));
            fallPlayer.Open(new Uri(currentPath + "/Resources/button-4.wav"));
            ringPlayer.Open(new Uri(currentPath + "/Resources/Ring.wav"));

        }

        public ImageSource SkeletonImageSource
        {
            get
            {
                return this.SkeletonimageSource;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
            }

            // 배경음악 실행
            PlaybackMusic();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }

        // 배경음악 실행 및 루프
        #region Background Music
        public void PlaybackMusic()
        {
            if (gamemusicPlayer != null)
            {
                gamemusicPlayer.Open(new Uri(currentPath + "/Resources/BackgroundMusic.WAV"));
                gamemusicPlayer.MediaEnded += new EventHandler(Media_Ended);
                gamemusicPlayer.Play();

                return;
            }
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            gamemusicPlayer.Position = TimeSpan.Zero;
            gamemusicPlayer.Play();
        }
        #endregion

        // 게임 오버
        private void GameComplete()
        {
            gamecompletePlayer.Play();
            gamemusicPlayer.Stop();

            bestTimeLeft = (timeLeft[0] + timeLeft[1] + timeLeft[2]) / 3;
            bestTimeRight = (timeRight[0] + timeRight[1] + timeRight[2]) / 3;
            finalbsLeft = (bsLeft[0] + bsLeft[1] + bsLeft[2]) / 3;
            finalbsRight = (bsRight[0] + bsRight[1] + bsRight[2]) / 3;

            EndWindow endWindow = new EndWindow();
            this.Close();
            endWindow.Show();
        }

        #region COM Method
        System.Windows.Media.Brush positionBrush = new SolidColorBrush(Colors.LightBlue);
        System.Windows.Media.Brush comBrush = new SolidColorBrush(Colors.Blue);

        CameraSpacePoint relativeGravityCenter(CameraSpacePoint origin, CameraSpacePoint point, float weight)
        {
            CameraSpacePoint updated = new CameraSpacePoint();
            updated.X = origin.X + point.X * weight;
            updated.Y = origin.Y + point.Y * weight;
            updated.Z = origin.Z + point.Z * weight;

            return updated;
        }

        void addPoint(CameraSpacePoint position)
        {
            ColorSpacePoint j2P = kinectSensor.CoordinateMapper.MapCameraPointToColorSpace(position);

            Ellipse myEllipse = new Ellipse();
            myEllipse.Fill = positionBrush;
            myEllipse.StrokeThickness = 5;
            myEllipse.Stroke = comBrush;
            myEllipse.Width = 40;
            myEllipse.Height = 40;
            Canvas.SetTop(myEllipse, j2P.Y - 225);
            Canvas.SetLeft(myEllipse, j2P.X - 225);
            skeletonCanvas.Children.Add(myEllipse);

            if (skeletonCanvas.Children.Count > 20)
                skeletonCanvas.Children.RemoveAt(0);
        }

        CameraSpacePoint middlePoint(Joint joint1, Joint joint2)
        {
            CameraSpacePoint point = new CameraSpacePoint();
            point.X = joint1.Position.X + joint2.Position.X;
            point.X /= 2;
            point.Y = joint1.Position.Y + joint2.Position.Y;
            point.Y /= 2;
            point.Z = joint1.Position.Z + joint2.Position.Z;
            point.Z /= 2;
            return point;
        }

        Double distance(CameraSpacePoint p1, CameraSpacePoint p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y) + (p1.Z - p2.Z) * (p1.Z - p2.Z);
        }
        #endregion

        // 스켈레톤 트래킹 (다리 드는 것 감지도 포함)
        #region Skeleton Video


        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            string message1 = " No Skeleton Data ";
            string message2 = " No Skeleton Data ";

            bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {

                using (DrawingContext dc = this.drawingGroup.Open())
                {
                    dc.DrawRectangle(Brushes.Transparent, null, new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));

                    int penIndex = 0;
                    foreach (Body body in this.bodies)
                    {
                        Pen drawPen = this.bodyColors[penIndex++];

                        if (texthelper)
                            if (!body.IsTracked)
                                StandStatusTextBlock.Text = "Please stand\non the center\nof screen";

                        if (body.IsTracked)
                        {
                            texthelper = false;
                            // 각 관절의 xyz 위치 변수 설정
                            Joint headJoint = body.Joints[JointType.Head];
                            Joint spineJoint = body.Joints[JointType.SpineBase];
                            Joint handleftJoint = body.Joints[JointType.HandLeft];
                            Joint handrightJoint = body.Joints[JointType.HandRight];
                            Joint ankleleftJoint = body.Joints[JointType.AnkleLeft];
                            Joint anklerightJoint = body.Joints[JointType.AnkleRight];
                            Joint footleftJoint = body.Joints[JointType.FootLeft];
                            Joint footrightJoint = body.Joints[JointType.FootRight];

                            CameraSpacePoint headPosition = headJoint.Position;
                            CameraSpacePoint spinePosition = spineJoint.Position;
                            CameraSpacePoint handleftPosition = handleftJoint.Position;
                            CameraSpacePoint handrightPosition = handrightJoint.Position;
                            CameraSpacePoint ankleleftPosition = ankleleftJoint.Position;
                            CameraSpacePoint anklerightPosition = anklerightJoint.Position;
                            CameraSpacePoint footleftPosition = footleftJoint.Position;
                            CameraSpacePoint footrightPosition = footrightJoint.Position;

                            // 왼발 오른발 좌표 표시
                            message1 = string.Format(" Left Ankle: X:{0:0.00} Y:{1:0.00} Z:{2:0.00}", ankleleftPosition.X, ankleleftPosition.Y, ankleleftPosition.Z);
                            message2 = string.Format("Right Ankle: X:{0:0.00} Y:{1:0.00} Z:{2:0.00} ", anklerightPosition.X, anklerightPosition.Y, anklerightPosition.Z);

                            TimeTextBlock.Text = TimeProgress.Value + " s";

                            if (trialNo % 2 == 0)
                                BSTextBlock.Text = Math.Round(balanceScoreLeft,2) + " %";
                            else
                                BSTextBlock.Text = Math.Round(balanceScoreRight,2) + " %";

                            #region COM
                            // Gravity center
                            CameraSpacePoint position = new CameraSpacePoint();
                            position = relativeGravityCenter(position, headPosition, (float)6.94);
                            position = relativeGravityCenter(position, spinePosition, (float)43.46);
                            position = relativeGravityCenter(position, handrightPosition, (float)0.305);
                            position = relativeGravityCenter(position, handleftPosition, (float)0.305);
                            position = relativeGravityCenter(position, footrightPosition, (float)0.685);
                            position = relativeGravityCenter(position, footleftPosition, (float)0.685);
                            position = relativeGravityCenter(position, middlePoint(body.Joints[JointType.WristLeft], body.Joints[JointType.ElbowLeft]), (float)0.81);
                            position = relativeGravityCenter(position, middlePoint(body.Joints[JointType.WristRight], body.Joints[JointType.ElbowRight]), (float)0.81);
                            position = relativeGravityCenter(position, middlePoint(body.Joints[JointType.ShoulderRight], body.Joints[JointType.ElbowRight]), (float)1.355);
                            position = relativeGravityCenter(position, middlePoint(body.Joints[JointType.ShoulderLeft], body.Joints[JointType.ElbowLeft]), (float)1.355);
                            position = relativeGravityCenter(position, middlePoint(body.Joints[JointType.AnkleLeft], body.Joints[JointType.KneeLeft]), (float)2.165);
                            position = relativeGravityCenter(position, middlePoint(body.Joints[JointType.AnkleRight], body.Joints[JointType.KneeRight]), (float)2.165);
                            position = relativeGravityCenter(position, middlePoint(body.Joints[JointType.AnkleLeft], body.Joints[JointType.HipLeft]), (float)7.08);
                            position = relativeGravityCenter(position, middlePoint(body.Joints[JointType.AnkleRight], body.Joints[JointType.HipRight]), (float)7.08);

                            addPoint(position);

                            // System.IO.File.AppendAllText("./result.txt", position.X + "," + position.Y + "," + position.Z + "\n", Encoding.Default);

                            if (gravities.Count == 0) gravities.Add(position);
                            else
                            {
                                if (distance(gravities[gravities.Count - 1], position) > 100.0)
                                {
                                    if (gravities.Count > 10)
                                    {
                                        gravities.Clear();
                                    }
                                    gravities.Add(position);
                                }

                                if (distance(gravities[gravities.Count - 1], position) > 17.0)
                                {
                                    if (!isPause && Math.Abs(anklerightPosition.Y - ankleleftPosition.Y) >= 0.1)
                                    {
                                        if (trialNo % 2 == 0)
                                            balanceScoreLeft = balanceScoreLeft - 0.2;
                                        else if (trialNo % 2 == 1)
                                            balanceScoreRight = balanceScoreRight - 0.2;
                                    }
                                }

                            }

                            double dist = distance(gravities[gravities.Count - 1], position);

                            if (!isPause && Math.Abs(anklerightPosition.Y - ankleleftPosition.Y) >= 0.1)
                            {
                                StatusProgress.Value = (100 - dist) / 100;
                            }
                            else
                                StatusProgress.Value = 1;

                            #endregion

                            if (isPause)
                                StandStatusTextBlock.Text = "Raise both\nhands high\nonce to start";

                            if (!isPause)
                            {
                                // 왼발로 서기
                                if (TimeProgress.Value == 0 && isTimerOn == 0 && trialNo == 0)
                                {

                                    StandStatusTextBlock.Text = "Stand on your\nleft limb\n(1st trial)";

                                    if (anklerightPosition.Y - ankleleftPosition.Y >= 0.1)
                                    {
                                        isFall = false;

                                        StandStatusTextBlock.Text = "Keep balance\nas long as\nyou can";

                                        _time = TimeSpan.FromSeconds(1);
                                        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                                        {
                                            TimeProgress.Value = _time.TotalSeconds;
                                            if (_time == TimeSpan.Zero) _timer.Stop();
                                            _time = _time.Add(TimeSpan.FromSeconds(1));
                                        }, Application.Current.Dispatcher);
                                        _timer.Start();

                                        isTimerOn++;
                                        gravities.Add(position);
                                        TimesPlayer.Play();
                                    }
                                }

                                if (TimeProgress.Value == 0 && isTimerOn == 2 && trialNo == 2 && !_timer.IsEnabled)
                                {

                                    StandStatusTextBlock.Text = "Stand on your\nleft limb\n(2nd trial)";

                                    if (anklerightPosition.Y - ankleleftPosition.Y >= 0.1)
                                    {
                                        isFall = false;

                                        StandStatusTextBlock.Text = "Keep balance\nas long as\nyou can";

                                        _time = TimeSpan.FromSeconds(1);
                                        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                                        {
                                            TimeProgress.Value = _time.TotalSeconds;
                                            if (_time == TimeSpan.Zero) _timer.Stop();
                                            _time = _time.Add(TimeSpan.FromSeconds(1));
                                        }, Application.Current.Dispatcher);
                                        _timer.Start();

                                        isTimerOn++; // isTimerOn의 숫자를 1 올림
                                        gravities.Add(position);
                                        TimesPlayer.Position = TimeSpan.Zero;
                                        TimesPlayer.Play();
                                    }
                                }

                                if (TimeProgress.Value == 0 && isTimerOn == 4 && trialNo == 4 && !_timer.IsEnabled)
                                {

                                    StandStatusTextBlock.Text = "Stand on your\nleft limb\n(3rd trial)";

                                    if (anklerightPosition.Y - ankleleftPosition.Y >= 0.1)
                                    {
                                        isFall = false;

                                        StandStatusTextBlock.Text = "Keep balance\nas long as\nyou can";

                                        _time = TimeSpan.FromSeconds(1);
                                        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                                        {
                                            TimeProgress.Value = _time.TotalSeconds;
                                            if (_time == TimeSpan.Zero) _timer.Stop();
                                            _time = _time.Add(TimeSpan.FromSeconds(1));
                                        }, Application.Current.Dispatcher);
                                        _timer.Start();

                                        isTimerOn++; // isTimerOn의 숫자를 1 올림
                                        gravities.Add(position);
                                        TimesPlayer.Position = TimeSpan.Zero;
                                        TimesPlayer.Play();
                                    }
                                }

                                if (TimeProgress.Value == 0 && isTimerOn == 1 && trialNo == 1 && !_timer.IsEnabled)
                                {
                                    StandStatusTextBlock.Text = "Stand on your\nright limb\n(1st trial)";

                                    if (ankleleftPosition.Y - anklerightPosition.Y >= 0.1)
                                    {
                                        isFall = false;

                                        StandStatusTextBlock.Text = "Keep balance\nas long as\nyou can";

                                        _time = TimeSpan.FromSeconds(1);
                                        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                                        {
                                            TimeProgress.Value = _time.TotalSeconds;
                                            if (_time == TimeSpan.Zero) _timer.Stop();
                                            _time = _time.Add(TimeSpan.FromSeconds(1));
                                        }, Application.Current.Dispatcher);
                                        _timer.Start();

                                        isTimerOn++;
                                        gravities.Add(position);
                                        TimesPlayer.Position = TimeSpan.Zero;
                                        TimesPlayer.Play();
                                    }
                                }

                                if (TimeProgress.Value == 0 && isTimerOn == 3 && trialNo == 3 && !_timer.IsEnabled)
                                {
                                    StandStatusTextBlock.Text = "Stand on your\nright limb\n(2nd trial)";

                                    if (ankleleftPosition.Y - anklerightPosition.Y >= 0.1)
                                    {
                                        isFall = false;

                                        StandStatusTextBlock.Text = "Keep balance\nas long as\nyou can";

                                        _time = TimeSpan.FromSeconds(1);
                                        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                                        {
                                            TimeProgress.Value = _time.TotalSeconds;
                                            if (_time == TimeSpan.Zero) _timer.Stop();
                                            _time = _time.Add(TimeSpan.FromSeconds(1));
                                        }, Application.Current.Dispatcher);
                                        _timer.Start();

                                        isTimerOn++;
                                        gravities.Add(position);
                                        TimesPlayer.Position = TimeSpan.Zero;
                                        TimesPlayer.Play();
                                    }
                                }

                                if (TimeProgress.Value == 0 && isTimerOn == 5 && trialNo == 5 && !_timer.IsEnabled)
                                {
                                    StandStatusTextBlock.Text = "Stand on your\nright limb\n(3rd trial)";

                                    if (ankleleftPosition.Y - anklerightPosition.Y >= 0.1)
                                    {
                                        isFall = false;

                                        StandStatusTextBlock.Text = "Keep balance\nas long as\nyou can";

                                        _time = TimeSpan.FromSeconds(1);
                                        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                                        {
                                            TimeProgress.Value = _time.TotalSeconds;
                                            if (_time == TimeSpan.Zero) _timer.Stop();
                                            _time = _time.Add(TimeSpan.FromSeconds(1));
                                        }, Application.Current.Dispatcher);
                                        _timer.Start();

                                        isTimerOn++;
                                        gravities.Add(position);
                                        TimesPlayer.Position = TimeSpan.Zero;
                                        TimesPlayer.Play();
                                    }
                                }

                                if (TimeProgress.Value == 0 && isTimerOn == 6 && trialNo == 6 && !_timer.IsEnabled)
                                {
                                    isTimerOn = 7;
                                    GameComplete();
                                    break;
                                }

                                // 타이머가 시작과 끝이 아닐 때 (진행 도중일 때) 왼발과 오른발의 차이가 0.03 이하일 경우 실패로 간주
                                if (TimeProgress.Value != 0 && isTimerOn != 0)
                                {
                                    if (isTimerOn % 2 == 1)
                                    {
                                        if (anklerightPosition.Y - ankleleftPosition.Y < 0.03 || TimeProgress.Value == 45)
                                        {
                                            if (!isFall)
                                            {
                                                if (trialNo == 0)
                                                {
                                                    timeLeft[0] = TimeProgress.Value;
                                                    bsLeft[0] = balanceScoreLeft;
                                                }
                                                if (trialNo == 2)
                                                {
                                                    timeLeft[1] = TimeProgress.Value;
                                                    bsLeft[1] = balanceScoreLeft;
                                                }
                                                if (trialNo == 4)
                                                {
                                                    timeLeft[2] = TimeProgress.Value;
                                                    bsLeft[2] = balanceScoreLeft;
                                                }

                                                isFall = true;
                                                _time = TimeSpan.Zero;
                                                fallPlayer.Position = TimeSpan.Zero;
                                                fallPlayer.Play();

                                                trialNo++;
                                                balanceScoreLeft = 100;

                                            }
                                        }
                                    }

                                    else
                                    {
                                        if (ankleleftPosition.Y - anklerightPosition.Y < 0.03 || TimeProgress.Value == 45)
                                        {
                                            if (!isFall)
                                            {
                                                if (trialNo == 1)
                                                {
                                                    timeRight[0] = TimeProgress.Value;
                                                    bsRight[0] = balanceScoreRight;
                                                }
                                                if (trialNo == 3)
                                                {
                                                    timeRight[1] = TimeProgress.Value;
                                                    bsRight[1] = balanceScoreRight;
                                                }
                                                if (trialNo == 5)
                                                {
                                                    timeRight[2] = TimeProgress.Value;
                                                    bsRight[2] = balanceScoreRight;
                                                }

                                                isFall = true;
                                                _time = TimeSpan.Zero;
                                                fallPlayer.Position = TimeSpan.Zero;
                                                fallPlayer.Play();

                                                trialNo++;
                                                balanceScoreRight = 100;

                                            }
                                        }
                                    }
                                }


                            }

                            // 두손을 높게 한 번 올리면 게임 시작
                            if (handleftPosition.Y >= 0.7 && handrightPosition.Y >= 0.7)
                            {
                                if (!isPause)
                                {
                                    isPause = true;
                                    if (isTimerOn != 0)
                                        _timer.Stop();
                                    Thread.Sleep(1000);
                                }

                                else
                                {
                                    isPause = false;
                                    if (isTimerOn != 0)
                                        _timer.Start();
                                    Thread.Sleep(1000);
                                }
                            }

                        }


                        IReadOnlyDictionary<JointType, Joint> joints = body.Joints;
                        Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                        foreach (JointType jointType in joints.Keys)
                        {
                            CameraSpacePoint position = joints[jointType].Position;
                            if (position.Z < 0)
                            {
                                position.Z = InferredZPositionClamp;
                            }

                            DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                            jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                        }

                        this.DrawBody(joints, jointPoints, dc, drawPen);
                    }
                }
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));

                FootLeftTextBlock.Text = message1;
                FootRightTextBlock.Text = message2;

            }
        }


        // Draw a body
        private void DrawBody(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in this.bones)
            {
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;

                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;

                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                    
                }
            }
        }

        // Draw one bone of a body (joint to joint)
        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            Pen drawPen = this.inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }





    }
}

#endregion