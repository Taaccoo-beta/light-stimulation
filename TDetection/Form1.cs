using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XControl;
using SerieslizeControlModule;
using System.Runtime.InteropServices;
using System.Threading;

namespace TDetection
{
    public partial class Form1 : Form
    {

        [DllImport("winmm")]
        static extern uint timeGetTime();

        [DllImport("winmm")]
        static extern void timeBeginPeriod(int t);
        [DllImport("winmm")]
        static extern void timeEndPeriod(int t);



        public Form1()
        {
            InitializeComponent();



        }

        private int lastTime;
        private int intervalTime;
        private int circle;
        private double voltage;



        private bool firstJump = true;
        private int count;
        private int circleCount;


        private bool isStart_1 = true;
        private bool isStart_2 = true;
        private bool isStart_3 = true;
        private bool isStart_4 = true;

        private bool runLastTime = true;
        private bool runIntervalTime = true;

        private bool isLastTimeSingCount = true;
        private bool isIntervalSingCount = true;


        private PortControl board = new PortControl(0);


        private int timeIndex = 0;
        private int secondIndex = 0;



        //---sign
        private int signCount = 0;
        private int timer2SingCount = 0;

        private Thread t;

        private bool CountSwitch;

        private void Form1_Load(object sender, EventArgs e)
        {

            board.AnalogPortConfigurationOut();
            board.DigitalConfigurationOut();
            timer1.Interval = 100;
            timer2.Interval = 100;
            board.DigitOutput(0, MccDaq.DigitalLogicState.Low);
            CoreSerialize cs = new CoreSerialize();
           
            TParameters dataT = new TParameters();

            try
            {
               
                dataT = cs.TParamDeSerializeNow();

                tbLastTime_1.Text = dataT.lastTime_1.ToString();
                tbIntervalTime_1.Text = dataT.intervalTime_1.ToString();
                tbCircle_1.Text = dataT.circle_1.ToString();
                tbVoltage_1.Text = dataT.voltage_1.ToString();

                tbLastTime_2.Text = dataT.lastTime_2.ToString();
                tbIntervalTime_2.Text = dataT.intervalTime_2.ToString();
                tbCircle_2.Text = dataT.circle_2.ToString();
                tbVoltage_2.Text = dataT.voltage_2.ToString();

                tbLastTime_3.Text = dataT.lastTime_3.ToString();
                tbIntervalTime_3.Text = dataT.intervalTime_3.ToString();
                tbCircle_3.Text = dataT.circle_3.ToString();
                tbVoltage_3.Text = dataT.voltage_3.ToString();

                tbLastTime_4.Text = dataT.lastTime_4.ToString();
                tbIntervalTime_4.Text = dataT.intervalTime_4.ToString();
                tbCircle_4.Text = dataT.circle_4.ToString();
                tbVoltage_4.Text = dataT.voltage_4.ToString();
            }
            catch
            {
                TParameters tp = new TParameters()
                {
                    lastTime_1 = 3,
                    intervalTime_1 = 3,
                    circle_1 = 3,
                    voltage_1 = 3.5,

                    lastTime_2 = 3,
                    intervalTime_2 = 3,
                    circle_2 = 3,
                    voltage_2 = 3.5,

                    lastTime_3 = 3,
                    intervalTime_3 = 3,
                    circle_3 = 3,
                    voltage_3 = 3.5,

                    lastTime_4 = 3,
                    intervalTime_4 = 3,
                    circle_4 = 3,
                    voltage_4 = 3.5,


                };

                cs.TParammSerializeNow(tp);

                dataT = cs.TParamDeSerializeNow();
                tbLastTime_1.Text = dataT.lastTime_1.ToString();
                tbIntervalTime_1.Text = dataT.intervalTime_1.ToString();
                tbCircle_1.Text = dataT.circle_1.ToString();
                tbVoltage_1.Text = dataT.voltage_1.ToString();

                tbLastTime_2.Text = dataT.lastTime_2.ToString();
                tbIntervalTime_2.Text = dataT.intervalTime_2.ToString();
                tbCircle_2.Text = dataT.circle_2.ToString();
                tbVoltage_2.Text = dataT.voltage_2.ToString();

                tbLastTime_3.Text = dataT.lastTime_3.ToString();
                tbIntervalTime_3.Text = dataT.intervalTime_3.ToString();
                tbCircle_3.Text = dataT.circle_3.ToString();
                tbVoltage_3.Text = dataT.voltage_3.ToString();

                tbLastTime_4.Text = dataT.lastTime_4.ToString();
                tbIntervalTime_4.Text = dataT.intervalTime_4.ToString();
                tbCircle_4.Text = dataT.circle_4.ToString();
                tbVoltage_4.Text = dataT.voltage_4.ToString();

            }


            t = new Thread(myTimer);


        }
        //private int lastTime;
        //private int intervalTime;
        //private int circle;
        //private double voltage;
        private int testcount = 0;


        private string lblShowSignText="NULL";
        private string lblShowValueText = "NULL";
        private string lblShowCountText = "NULL";
        private string btnStart_1Text = "Start";
        private string btnStart_2Text = "Start";
        private string btnStart_3Text = "Start";
        private string btnStart_4Text = "Start";
        private bool isMyTimerFinished = false;
        private bool isTheardCircleClosed = false;



        private void myTimer()
        {
            timeBeginPeriod(1);
            uint start = timeGetTime();
            uint newStart;
            bool ifWorkFinished = false;
            
            while (!isTheardCircleClosed)
            {
               
                if (!ifWorkFinished)
                {

                    if (circleCount < circle)
                    {
                        if (runLastTime)
                        {
                            CountSwitch = true;
                            if (signCount < 2)
                            {
                                board.DigitOutput(0, MccDaq.DigitalLogicState.High);
                                lblShowSignText = "High";
                            }
                            else
                            {
                                board.DigitOutput(0, MccDaq.DigitalLogicState.Low);
                                lblShowSignText = "Low";
                            }
                            
                            if (count < lastTime)
                            {
                                board.VOutput(0, Convert.ToSingle(voltage));
                                lblShowValueText = voltage.ToString();
                                lblShowCountText = "LastPhase";
                            }
                            else
                            {
                                count = 0;
                                runIntervalTime = true;
                                runLastTime = false;
                                signCount = 0;
                                isLastTimeSingCount = false;
                                isIntervalSingCount = true;
                            }


                            if (isLastTimeSingCount)
                            {
                                signCount++;
                            }

                        }



                        if (runIntervalTime)
                        {
                            
                            if (count < intervalTime)
                            {
                                board.VOutput(0, 0);
                                lblShowValueText = "0";
                                lblShowCountText = "IntervalPhase";
                            }
                            else
                            {
                                count = 0;
                                runLastTime = true;
                                runIntervalTime = false;
                                signCount = 0;
                                circleCount++;
                                isLastTimeSingCount = true;
                                isIntervalSingCount = false;
                                CountSwitch = false;
                            }
                            

                        }


                        if (CountSwitch)
                        {
                            count++;
                        }
                        

                    }
                    else
                    {
                        isStart_1 = true;
                        isStart_2 = true;
                        isStart_3 = true;
                        isStart_4 = true;

                        btnStart_1Text = "Start";
                        btnStart_2Text = "Start";
                        btnStart_3Text = "Start";
                        btnStart_4Text = "Start";

                        lblShowValueText = "NULL";
                        lblShowCountText = "NULL";
                        lblShowSignText = "NULL";
                        isMyTimerFinished = true;
                        board.VOutput(0, 0);

                        isTheardCircleClosed = true;

                        

                    }
                    
                    ifWorkFinished = true;
                }

                newStart = timeGetTime();


                if (newStart - start >= 100)
                {
                    this.testcount++;
                    ifWorkFinished = false;
                    start = newStart;
                    timeIndex++;
                    if (timeIndex == 10)
                    {
                        timeIndex = 0;
                        secondIndex++;

                    }
                    
                   
                }

            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isMyTimerFinished == false)
            {
                this.lblShowCount.Text = lblShowCountText;
                this.lblShowSign.Text = lblShowSignText;
                this.lblShowValue.Text = lblShowValueText;
                this.lblShowTime.Text = secondIndex.ToString();
            }
            else
            {
                timer1.Stop();
                isStart_1 = true;
                isStart_2 = true;
                isStart_3 = true;
                isStart_4 = true;

                btnStart_1.Text = "Start";
                btnStart_2.Text = "Start";
                btnStart_3.Text = "Start";
                btnStart_4.Text = "Start";

                board.VOutput(0, 0);

                this.btnStart_1.Enabled = true;
                this.btnStart_2.Enabled = true;
                this.btnStart_3.Enabled = true;
                this.btnStart_4.Enabled = true;
                this.lblShowCount.Text = "NULL";
                this.lblShowValue.Text = "NULL";
                this.lblShowSign.Text = "NULL";
            }
            
            
        }

        private void btnStart_1_Click(object sender, EventArgs e)
        {
            if (isStart_1 == true)
            {
                isStart_1 = false;
                btnStart_1.Text = "Stop";

                lastTime = Convert.ToInt32(double.Parse(tbLastTime_1.Text) * 10);
                intervalTime = Convert.ToInt32(double.Parse(tbIntervalTime_1.Text) * 10);
                circle = int.Parse(tbCircle_1.Text);
                voltage = double.Parse(tbVoltage_1.Text);

                runLastTime = true;
                runIntervalTime = false;

                this.btnStart_2.Enabled = false;
                this.btnStart_3.Enabled = false;
                this.btnStart_4.Enabled = false;

                isLastTimeSingCount = true;
                isIntervalSingCount = true;

                count = 0;
                circleCount = 0;
                signCount = 0;

                isMyTimerFinished = false;

                timeIndex = 0;
                secondIndex = 0;

                t = new Thread(myTimer);
                isTheardCircleClosed = false;
                t.Start();
                timer1.Start();
            }
            else
            {
                isStart_1 = true;
                btnStart_1.Text = "Start";
                board.VOutput(0, 0);
                this.btnStart_2.Enabled = true;
                this.btnStart_3.Enabled = true;
                this.btnStart_4.Enabled = true;
                this.lblShowCount.Text = "NULL";
                this.lblShowValue.Text = "NULL";
                this.lblShowSign.Text = "NULL";
                timer1.Stop();
                isTheardCircleClosed = true;
                t.Abort();
                
            }



        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            CoreSerialize cs = new CoreSerialize();

           
            TParameters tp = new TParameters()
            {
                lastTime_1 = double.Parse(tbLastTime_1.Text),
                intervalTime_1 = double.Parse(tbIntervalTime_1.Text),
                circle_1 = int.Parse(tbCircle_1.Text),
                voltage_1 = double.Parse(tbVoltage_1.Text),

                lastTime_2 = double.Parse(tbLastTime_2.Text),
                intervalTime_2 = double.Parse(tbIntervalTime_2.Text),
                circle_2 = int.Parse(tbCircle_2.Text),
                voltage_2= double.Parse(tbVoltage_2.Text),

                lastTime_3 = double.Parse(tbLastTime_3.Text),
                intervalTime_3 = double.Parse(tbIntervalTime_3.Text),
                circle_3 = int.Parse(tbCircle_3.Text),
                voltage_3 = double.Parse(tbVoltage_3.Text),

                lastTime_4 = double.Parse(tbLastTime_4.Text),
                intervalTime_4 = double.Parse(tbIntervalTime_4.Text),
                circle_4= int.Parse(tbCircle_4.Text),
                voltage_4 = double.Parse(tbVoltage_4.Text),

            };

            cs.TParammSerializeNow(tp);
        }

        private void btnStart_2_Click(object sender, EventArgs e)
        {
            if (isStart_2 == true)
            {
                isStart_2 = false;
                btnStart_2.Text = "Stop";

                lastTime = Convert.ToInt32(double.Parse(tbLastTime_2.Text) * 10);
                intervalTime = Convert.ToInt32(double.Parse(tbIntervalTime_2.Text) * 10);
                circle = int.Parse(tbCircle_2.Text);
                voltage = double.Parse(tbVoltage_2.Text);

                this.btnStart_1.Enabled = false;
                this.btnStart_3.Enabled = false;
                this.btnStart_4.Enabled = false;

                isLastTimeSingCount = true;
                isIntervalSingCount = true;

                runLastTime = true;
                runIntervalTime = false;

                circleCount = 0;
                count = 0;
                signCount = 0;
                timeIndex = 0;
                secondIndex = 0;
                isMyTimerFinished = false;
                t = new Thread(myTimer);
                isTheardCircleClosed = false;
                t.Start();
                timer1.Start();
            }
            else
            {
                isStart_2 = true;
                btnStart_2.Text = "Start";
                board.VOutput(0, 0);
                this.lblShowCount.Text = "NULL";
                this.lblShowValue.Text = "NULL";
                this.lblShowSign.Text = "NULL";

                this.btnStart_1.Enabled = true;
                this.btnStart_3.Enabled = true;
                this.btnStart_4.Enabled = true;
                timer1.Stop();
                isTheardCircleClosed = true;
                t.Abort();
            }
        }

        private void btnStart_3_Click(object sender, EventArgs e)
        {
            if (isStart_3 == true)
            {
                isStart_3 = false;
                btnStart_3.Text = "Stop";

                lastTime = Convert.ToInt32(double.Parse(tbLastTime_3.Text) * 10);
                intervalTime = Convert.ToInt32(double.Parse(tbIntervalTime_3.Text) * 10);
                circle = int.Parse(tbCircle_3.Text);
                voltage = double.Parse(tbVoltage_3.Text);

                this.btnStart_1.Enabled = false;
                this.btnStart_2.Enabled = false;
                this.btnStart_4.Enabled = false;

                isLastTimeSingCount = true;
                isIntervalSingCount = true;

                runLastTime = true;
                runIntervalTime = false;

                circleCount = 0;
                count = 0;
                signCount = 0;
                secondIndex = 0;
                isMyTimerFinished = false;
                timeIndex = 0;
                t = new Thread(myTimer);
                isTheardCircleClosed = false;
                t.Start();
                timer1.Start();
            }
            else
            {
                isStart_3 = true;
                btnStart_3.Text = "Start";
                board.VOutput(0, 0);
                this.btnStart_1.Enabled = true;
                this.btnStart_2.Enabled = true;
                this.btnStart_4.Enabled = true;
                this.lblShowCount.Text = "NULL";
                this.lblShowValue.Text = "NULL";
                this.lblShowSign.Text = "NULL";
                timer1.Stop();
                isTheardCircleClosed = true;
                t.Abort();

            }
        }

        private void btnStart_4_Click(object sender, EventArgs e)
        {
            if (isStart_4 == true)
            {
                isStart_4 = false;
                btnStart_4.Text = "Stop";

                lastTime = Convert.ToInt32(double.Parse(tbLastTime_4.Text) * 10);
                intervalTime = Convert.ToInt32(double.Parse(tbIntervalTime_4.Text) * 10);
                circle = int.Parse(tbCircle_4.Text);
                voltage = double.Parse(tbVoltage_4.Text);

                this.btnStart_1.Enabled = false;
                this.btnStart_3.Enabled = false;
                this.btnStart_2.Enabled = false;

                isLastTimeSingCount = true;
                isIntervalSingCount = true;

                runLastTime = true;
                runIntervalTime = false;

                circleCount = 0;
                count = 0;
                signCount = 0;
                secondIndex = 0;
                isMyTimerFinished = false;
                timeIndex = 0;
                t = new Thread(myTimer);
                isTheardCircleClosed = false;
                t.Start();
                timer1.Start();
            }
            else
            {
                isStart_4 = true;
                btnStart_4.Text = "Start";
                board.VOutput(0, 0);
                this.btnStart_1.Enabled = true;
                this.btnStart_3.Enabled = true;
                this.btnStart_2.Enabled = true;
                this.lblShowCount.Text = "NULL";
                this.lblShowValue.Text = "NULL";
                this.lblShowSign.Text = "NULL";
                timer1.Stop();
                isTheardCircleClosed = true;
                t.Abort();

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            board.VOutput(0, 0);
            isTheardCircleClosed = true;
            t.Abort();
            timer1.Stop();
        }
    }
}
