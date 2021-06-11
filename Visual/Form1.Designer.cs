
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
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearStep = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCellSize = new System.Windows.Forms.TextBox();
            this.pnMaze = new Visual.MazeControl();
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
            this.groupBox1.Location = new System.Drawing.Point(1002, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 158);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Draw";
            // 
            // rbtDelete
            // 
            this.rbtDelete.AutoSize = true;
            this.rbtDelete.Location = new System.Drawing.Point(7, 122);
            this.rbtDelete.Name = "rbtDelete";
            this.rbtDelete.Size = new System.Drawing.Size(69, 23);
            this.rbtDelete.TabIndex = 0;
            this.rbtDelete.Text = "Delete";
            this.rbtDelete.UseVisualStyleBackColor = true;
            this.rbtDelete.CheckedChanged += new System.EventHandler(this.rbtDelete_CheckedChanged);
            // 
            // rbtDrawGoal
            // 
            this.rbtDrawGoal.AutoSize = true;
            this.rbtDrawGoal.Location = new System.Drawing.Point(7, 93);
            this.rbtDrawGoal.Name = "rbtDrawGoal";
            this.rbtDrawGoal.Size = new System.Drawing.Size(94, 23);
            this.rbtDrawGoal.TabIndex = 0;
            this.rbtDrawGoal.Text = "Draw Goal";
            this.rbtDrawGoal.UseVisualStyleBackColor = true;
            this.rbtDrawGoal.CheckedChanged += new System.EventHandler(this.rbtDrawGoal_CheckedChanged);
            // 
            // rbtDrawStart
            // 
            this.rbtDrawStart.AutoSize = true;
            this.rbtDrawStart.Location = new System.Drawing.Point(6, 64);
            this.rbtDrawStart.Name = "rbtDrawStart";
            this.rbtDrawStart.Size = new System.Drawing.Size(95, 23);
            this.rbtDrawStart.TabIndex = 0;
            this.rbtDrawStart.Text = "Draw Start";
            this.rbtDrawStart.UseVisualStyleBackColor = true;
            this.rbtDrawStart.CheckedChanged += new System.EventHandler(this.rbtDrawStart_CheckedChanged);
            // 
            // rbtDrawWall
            // 
            this.rbtDrawWall.AutoSize = true;
            this.rbtDrawWall.Checked = true;
            this.rbtDrawWall.Location = new System.Drawing.Point(6, 35);
            this.rbtDrawWall.Name = "rbtDrawWall";
            this.rbtDrawWall.Size = new System.Drawing.Size(91, 23);
            this.rbtDrawWall.TabIndex = 0;
            this.rbtDrawWall.TabStop = true;
            this.rbtDrawWall.Text = "Draw Wall";
            this.rbtDrawWall.UseVisualStyleBackColor = true;
            this.rbtDrawWall.CheckedChanged += new System.EventHandler(this.rbtDrawWall_CheckedChanged);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(1002, 423);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(196, 39);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(1002, 468);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(196, 39);
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
            this.groupBox2.Location = new System.Drawing.Point(1002, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 136);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Heuristic";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 92);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(144, 23);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Text = "Euclidean Heuristic";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 63);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(141, 23);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.Text = "Diagonal Heuristic";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 34);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(155, 23);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Manhattan Heuristic";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // tbrSleep
            // 
            this.tbrSleep.LargeChange = 1;
            this.tbrSleep.Location = new System.Drawing.Point(1050, 361);
            this.tbrSleep.Maximum = 5;
            this.tbrSleep.Name = "tbrSleep";
            this.tbrSleep.Size = new System.Drawing.Size(148, 56);
            this.tbrSleep.TabIndex = 1;
            this.tbrSleep.Scroll += new System.EventHandler(this.tbrSleep_Scroll);
            // 
            // lbStep
            // 
            this.lbStep.Location = new System.Drawing.Point(998, 651);
            this.lbStep.Name = "lbStep";
            this.lbStep.Size = new System.Drawing.Size(200, 24);
            this.lbStep.TabIndex = 6;
            this.lbStep.Text = "Step: 0";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbTime
            // 
            this.lbTime.Location = new System.Drawing.Point(998, 687);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(200, 30);
            this.lbTime.TabIndex = 7;
            this.lbTime.Text = "Time: 0 second";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1002, 558);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(196, 39);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear All";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(998, 361);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Delay:";
            // 
            // btnClearStep
            // 
            this.btnClearStep.Location = new System.Drawing.Point(1002, 513);
            this.btnClearStep.Name = "btnClearStep";
            this.btnClearStep.Size = new System.Drawing.Size(196, 39);
            this.btnClearStep.TabIndex = 8;
            this.btnClearStep.Text = "Clear Step";
            this.btnClearStep.UseVisualStyleBackColor = true;
            this.btnClearStep.Click += new System.EventHandler(this.btnClearStep_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(998, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "Cell Size:";
            // 
            // txtCellSize
            // 
            this.txtCellSize.Location = new System.Drawing.Point(1065, 16);
            this.txtCellSize.Name = "txtCellSize";
            this.txtCellSize.Size = new System.Drawing.Size(133, 25);
            this.txtCellSize.TabIndex = 11;
            this.txtCellSize.Text = "30";
            this.txtCellSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCellSize_KeyDown);
            // 
            // pnMaze
            // 
            this.pnMaze.BackColor = System.Drawing.Color.White;
            this.pnMaze.Location = new System.Drawing.Point(10, 13);
            this.pnMaze.Name = "pnMaze";
            this.pnMaze.Size = new System.Drawing.Size(982, 735);
            this.pnMaze.TabIndex = 0;
            this.pnMaze.Paint += new System.Windows.Forms.PaintEventHandler(this.pnMaze_Paint);
            this.pnMaze.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnMaze_MouseDown);
            this.pnMaze.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnMaze_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1210, 761);
            this.Controls.Add(this.txtCellSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClearStep);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbStep);
            this.Controls.Add(this.tbrSleep);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnMaze);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AStart Visualization";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearStep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCellSize;
    }
}

