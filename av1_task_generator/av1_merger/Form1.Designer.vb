<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ivfDirectoryTxtBox = New System.Windows.Forms.TextBox()
        Me.OpusFileTxtBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ConcatTxtFileTxtBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.OutputFileTxtBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.BrowseOutputBtn = New System.Windows.Forms.Button()
        Me.StartBtn = New System.Windows.Forms.Button()
        Me.OutputFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(412, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Drag and drop the folder with the .ivf files, the .opus file, and the .txt concat" &
    "enation file"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = ".ivf directory:"
        '
        'ivfDirectoryTxtBox
        '
        Me.ivfDirectoryTxtBox.Location = New System.Drawing.Point(15, 54)
        Me.ivfDirectoryTxtBox.Name = "ivfDirectoryTxtBox"
        Me.ivfDirectoryTxtBox.Size = New System.Drawing.Size(409, 20)
        Me.ivfDirectoryTxtBox.TabIndex = 2
        '
        'OpusFileTxtBox
        '
        Me.OpusFileTxtBox.Location = New System.Drawing.Point(15, 95)
        Me.OpusFileTxtBox.Name = "OpusFileTxtBox"
        Me.OpusFileTxtBox.Size = New System.Drawing.Size(409, 20)
        Me.OpusFileTxtBox.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = ".opus file:"
        '
        'ConcatTxtFileTxtBox
        '
        Me.ConcatTxtFileTxtBox.Location = New System.Drawing.Point(15, 136)
        Me.ConcatTxtFileTxtBox.Name = "ConcatTxtFileTxtBox"
        Me.ConcatTxtFileTxtBox.Size = New System.Drawing.Size(409, 20)
        Me.ConcatTxtFileTxtBox.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = ".txt Concatenation file:"
        '
        'OutputFileTxtBox
        '
        Me.OutputFileTxtBox.Location = New System.Drawing.Point(15, 181)
        Me.OutputFileTxtBox.Name = "OutputFileTxtBox"
        Me.OutputFileTxtBox.Size = New System.Drawing.Size(328, 20)
        Me.OutputFileTxtBox.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 165)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Output Path:"
        '
        'BrowseOutputBtn
        '
        Me.BrowseOutputBtn.Location = New System.Drawing.Point(349, 178)
        Me.BrowseOutputBtn.Name = "BrowseOutputBtn"
        Me.BrowseOutputBtn.Size = New System.Drawing.Size(75, 23)
        Me.BrowseOutputBtn.TabIndex = 9
        Me.BrowseOutputBtn.Text = "Browse"
        Me.BrowseOutputBtn.UseVisualStyleBackColor = True
        '
        'StartBtn
        '
        Me.StartBtn.Location = New System.Drawing.Point(15, 207)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(409, 23)
        Me.StartBtn.TabIndex = 10
        Me.StartBtn.Text = "Start"
        Me.StartBtn.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(437, 245)
        Me.Controls.Add(Me.StartBtn)
        Me.Controls.Add(Me.BrowseOutputBtn)
        Me.Controls.Add(Me.OutputFileTxtBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ConcatTxtFileTxtBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.OpusFileTxtBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ivfDirectoryTxtBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "AV1 Merger"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ivfDirectoryTxtBox As TextBox
    Friend WithEvents OpusFileTxtBox As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ConcatTxtFileTxtBox As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents OutputFileTxtBox As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents BrowseOutputBtn As Button
    Friend WithEvents StartBtn As Button
    Friend WithEvents OutputFileDialog As SaveFileDialog
End Class
