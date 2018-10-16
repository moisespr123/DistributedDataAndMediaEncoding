<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.InputTxt = New System.Windows.Forms.TextBox()
        Me.OutputTxt = New System.Windows.Forms.TextBox()
        Me.InputBrowseBtn = New System.Windows.Forms.Button()
        Me.StartBtn = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.audioBitrate = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.BrowseTempLocation = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tempLocationPath = New System.Windows.Forms.TextBox()
        Me.speed = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.quantizer = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.UserKey = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout
        CType(Me.audioBitrate,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.speed,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.quantizer,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Step 1: Browse for an input file"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(12, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Step 2: Enter batch job name"
        '
        'InputTxt
        '
        Me.InputTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.InputTxt.Location = New System.Drawing.Point(15, 26)
        Me.InputTxt.Name = "InputTxt"
        Me.InputTxt.Size = New System.Drawing.Size(340, 20)
        Me.InputTxt.TabIndex = 2
        '
        'OutputTxt
        '
        Me.OutputTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.OutputTxt.Location = New System.Drawing.Point(15, 68)
        Me.OutputTxt.Name = "OutputTxt"
        Me.OutputTxt.Size = New System.Drawing.Size(415, 20)
        Me.OutputTxt.TabIndex = 3
        '
        'InputBrowseBtn
        '
        Me.InputBrowseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.InputBrowseBtn.Location = New System.Drawing.Point(361, 24)
        Me.InputBrowseBtn.Name = "InputBrowseBtn"
        Me.InputBrowseBtn.Size = New System.Drawing.Size(75, 23)
        Me.InputBrowseBtn.TabIndex = 5
        Me.InputBrowseBtn.Text = "Browse"
        Me.InputBrowseBtn.UseVisualStyleBackColor = true
        '
        'StartBtn
        '
        Me.StartBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.StartBtn.Location = New System.Drawing.Point(15, 267)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(424, 37)
        Me.StartBtn.TabIndex = 8
        Me.StartBtn.Text = "Start"
        Me.StartBtn.UseVisualStyleBackColor = true
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(15, 314)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Progress:"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(18, 331)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(421, 23)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(15, 368)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "GUI by Moises Cardona"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = true
        Me.Label6.Location = New System.Drawing.Point(411, 368)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "v1.0"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.audioBitrate)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.BrowseTempLocation)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.tempLocationPath)
        Me.GroupBox1.Controls.Add(Me.speed)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.quantizer)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 94)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(421, 122)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Encoder Options"
        '
        'audioBitrate
        '
        Me.audioBitrate.Location = New System.Drawing.Point(143, 37)
        Me.audioBitrate.Maximum = New Decimal(New Integer() {320, 0, 0, 0})
        Me.audioBitrate.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.audioBitrate.Name = "audioBitrate"
        Me.audioBitrate.Size = New System.Drawing.Size(65, 20)
        Me.audioBitrate.TabIndex = 19
        Me.audioBitrate.Value = New Decimal(New Integer() {64, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Location = New System.Drawing.Point(140, 21)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 13)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "audio bitrate:"
        '
        'BrowseTempLocation
        '
        Me.BrowseTempLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.BrowseTempLocation.Location = New System.Drawing.Point(340, 83)
        Me.BrowseTempLocation.Name = "BrowseTempLocation"
        Me.BrowseTempLocation.Size = New System.Drawing.Size(75, 23)
        Me.BrowseTempLocation.TabIndex = 17
        Me.BrowseTempLocation.Text = "Browse"
        Me.BrowseTempLocation.UseVisualStyleBackColor = true
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.Location = New System.Drawing.Point(9, 69)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(126, 13)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "BOINC Download Folder:"
        '
        'tempLocationPath
        '
        Me.tempLocationPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tempLocationPath.Location = New System.Drawing.Point(12, 85)
        Me.tempLocationPath.Name = "tempLocationPath"
        Me.tempLocationPath.Size = New System.Drawing.Size(322, 20)
        Me.tempLocationPath.TabIndex = 16
        '
        'speed
        '
        Me.speed.Location = New System.Drawing.Point(80, 37)
        Me.speed.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.speed.Name = "speed"
        Me.speed.Size = New System.Drawing.Size(50, 20)
        Me.speed.TabIndex = 3
        Me.speed.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Location = New System.Drawing.Point(77, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "speed:"
        '
        'quantizer
        '
        Me.quantizer.Location = New System.Drawing.Point(12, 37)
        Me.quantizer.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.quantizer.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.quantizer.Name = "quantizer"
        Me.quantizer.Size = New System.Drawing.Size(50, 20)
        Me.quantizer.TabIndex = 1
        Me.quantizer.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(9, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "quantizer:"
        '
        'UserKey
        '
        Me.UserKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.UserKey.Location = New System.Drawing.Point(15, 241)
        Me.UserKey.Name = "UserKey"
        Me.UserKey.Size = New System.Drawing.Size(421, 20)
        Me.UserKey.TabIndex = 17
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Location = New System.Drawing.Point(12, 224)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "User Key"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 410)
        Me.Controls.Add(Me.UserKey)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.StartBtn)
        Me.Controls.Add(Me.InputBrowseBtn)
        Me.Controls.Add(Me.OutputTxt)
        Me.Controls.Add(Me.InputTxt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = false
        Me.Name = "Form1"
        Me.Text = "Distributed Data and Media Encoding | AV1 Task Submitter"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        CType(Me.audioBitrate,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.speed,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.quantizer,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents InputTxt As TextBox
    Friend WithEvents OutputTxt As TextBox
    Friend WithEvents InputBrowseBtn As Button
    Friend WithEvents StartBtn As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents speed As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents quantizer As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents audioBitrate As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents BrowseTempLocation As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents tempLocationPath As TextBox
    Friend WithEvents UserKey As TextBox
    Friend WithEvents Label10 As Label
End Class
