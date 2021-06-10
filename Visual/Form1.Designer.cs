
namespace Visual
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtDelete = new System.Windows.Forms.RadioButton();
            this.rbtDrawGoal = new System.Windows.Forms.RadioButton();
            this.rbtDrawStart = new System.Windows.Forms.RadioButton();
            this.rbtDrawWall = new System.Windows.Forms.RadioButton();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.tbrSleep = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbStep = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbTime = new System.Windows.Forms.Label();
            this.pnMaze = new Visual.MazeControl();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSleep)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtDelete);
            this.groupBox1.Controls.Add(this.rbtDrawGoal);
            this.groupBox1.Controls.Add(this.rbtDrawStart);
            this.groupBox1.Controls.Add(this.rbtDrawWall);
            this.groupBox1.Location = new System.Drawing.Point(960, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 192);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Draw";
            // 
            // rbtDelete
            // 
            this.rbtDelete.AutoSize = true;
            this.rbtDelete.Location = new System.Drawing.Point(30, 156);
            this.rbtDelete.Name = "rbtDelete";
            this.rbtDelete.Size = new System.Drawing.Size(70, 21);
            this.rbtDelete.TabIndex = 0;
            this.rbtDelete.Text = "Delete";
            this.rbtDelete.UseVisualStyleBackColor = true;
            this.rbtDelete.CheckedChanged += new System.EventHandler(this.rbtDelete_CheckedChanged);
            // 
            // rbtDrawGoal
            // 
            this.rbtDrawGoal.AutoSize = true;
            this.rbtDrawGoal.Location = new System.Drawing.Point(30, 117);
            this.rbtDrawGoal.Name = "rbtDrawGoal";
            this.rbtDrawGoal.Size = new System.Drawing.Size(95, 21);
            this.rbtDrawGoal.TabIndex = 0;
            this.rbtDrawGoal.Text = "Draw Goal";
            this.rbtDrawGoal.UseVisualStyleBackColor = true;
            this.rbtDrawGoal.CheckedChanged += new System.EventHandler(this.rbtDrawGoal_CheckedChanged);
            // 
            // rbtDrawStart
            // 
            this.rbtDrawStart.AutoSize = true;
            this.rbtDrawStart.Location = new System.Drawing.Point(30, 77);
            this.rbtDrawStart.Name = "rbtDrawStart";
            this.rbtDrawStart.Size = new System.Drawing.Size(95, 21);
            this.rbtDrawStart.TabIndex = 0;
            this.rbtDrawStart.Text = "Draw Start";
            this.rbtDrawStart.UseVisualStyleBackColor = true;
            this.rbtDrawStart.CheckedChanged += new System.EventHandler(this.rbtDrawStart_CheckedChanged);
            // 
            // rbtDrawWall
            // 
            this.rbtDrawWall.AutoSize = true;
            this.rbtDrawWall.Checked = true;
            this.rbtDrawWall.Location = new System.Drawing.Point(30, 39);
            this.rbtDrawWall.Name = "rbtDrawWall";
            this.rbtDrawWall.Size = new System.Drawing.Size(92, 21);
            this.rbtDrawWall.TabIndex = 0;
            this.rbtDrawWall.TabStop = true;
            this.rbtDrawWall.Text = "Draw Wall";
            this.rbtDrawWall.UseVisualStyleBackColor = true;
            this.rbtDrawWall.CheckedChanged += new System.EventHandler(this.rbtDrawWall_CheckedChanged);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(980, 461);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(980, 504);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(960, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 171);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Heuristic";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(30, 106);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(96, 21);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Text = "Heuristic 3";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(30, 69);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(96, 21);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.Text = "Heuristic 2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(30, 33);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(96, 21);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Heuristic 1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // tbrSleep
            // 
            this.tbrSleep.LargeChange = 1;
            this.tbrSleep.Location = new System.Drawing.Point(961, 399);
            this.tbrSleep.Maximum = 5;
            this.tbrSleep.Minimum = 1;
            this.tbrSleep.Name = "tbrSleep";
            this.tbrSleep.Size = new System.Drawing.Size(256, 56);
            this.tbrSleep.TabIndex = 1;
            this.tbrSleep.Value = 1;
            this.tbrSleep.Scroll += new System.EventHandler(this.tbrSleep_Scroll);
            // 
            // lbStep
            // 
            this.lbStep.Location = new System.Drawing.Point(977, 596);
            this.lbStep.Name = "lbStep";
            this.lbStep.Size = new System.Drawing.Size(240, 23);
            this.lbStep.TabIndex = 6;
            this.lbStep.Text = "Step:";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbTime
            // 
            this.lbTime.Location = new System.Drawing.Point(977, 633);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(240, 28);
            this.lbTime.TabIndex = 7;
            this.lbTime.Text = "Time:";
            // 
            // pnMaze
            // 
            this.pnMaze.BackColor = System.Drawing.Color.White;
            this.pnMaze.Location = new System.Drawing.Point(12, 12);
            this.pnMaze.Name = "pnMaze";
            this.pnMaze.Size = new System.Drawing.Size(942, 692);
            this.pnMaze.TabIndex = 0;
            this.pnMaze.Paint += new System.Windows.Forms.PaintEventHandler(this.pnMaze_Paint);
            this.pnMaze.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnMaze_MouseDown);
            this.pnMaze.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnMaze_MouseMove);
            this.pnMaze.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnMaze_MouseUp);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(980, 546);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1229, 716);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbStep);
            this.Controls.Add(this.tbrSleep);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnMaze);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSleep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MazeControl pnMaze;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtDelete;
        private System.Windows.Forms.RadioButton rbtDrawGoal;
        private System.Windows.Forms.RadioButton rbtDrawStart;
        private System.Windows.Forms.RadioButton rbtDrawWall;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TrackBar tbrSleep;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbStep;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Button btnClear;
    }
}

