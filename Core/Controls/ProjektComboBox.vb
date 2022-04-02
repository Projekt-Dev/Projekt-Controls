Imports System.Drawing.Design
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<DefaultEvent("OnSelectedIndexChanged")>
Public Class ProjektComboBox
    Inherits UserControl

    'Fields
    Private _backColor As Color = Color.Crimson
    Private _iconColor As Color = Color.White
    Private _listBackColor As Color = Color.Crimson
    Private _listTextColor As Color = Color.White
    Private _borderColor As Color = Color.White
    Private _borderSize As Integer = 1

    'Items
    Private ReadOnly cmbList As ComboBox
    Private ReadOnly lblText As Label
    Private ReadOnly btnIcon As Button

#Region "Properties"
    <Category("Projekt")>
    Public Shadows Property BackColor As Color
        Get
            Return _backColor
        End Get
        Set(value As Color)
            _backColor = value
            lblText.BackColor = BackColor
            btnIcon.BackColor = BackColor
        End Set
    End Property
    <Category("Projekt")>
    Public Property IconColor As Color
        Get
            Return _iconColor
        End Get
        Set(value As Color)
            _iconColor = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property ListBackColor As Color
        Get
            Return _listBackColor
        End Get
        Set(value As Color)
            _listBackColor = value
            cmbList.BackColor = ListBackColor
        End Set
    End Property
    <Category("Projekt")>
    Public Property ListTextColor As Color
        Get
            Return _listTextColor
        End Get
        Set(value As Color)
            _listTextColor = value
            cmbList.ForeColor = ListTextColor
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            MyBase.BackColor = BorderColor
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(value As Integer)
            _borderSize = value
            MyBase.Padding = New Padding(BorderSize)
            AdjustComboBoxDimensions()
        End Set
    End Property
    <Category("Projekt")>
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As Color)
            MyBase.ForeColor = value
            lblText.ForeColor = value
        End Set
    End Property
    <Category("Projekt")>
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = value
            lblText.Font = value
            cmbList.Font = value
        End Set
    End Property
    <Category("Projekt")>
    Public Property _Text As String
        Get
            Return lblText.Text
        End Get
        Set(value As String)
            lblText.Text = value
        End Set
    End Property
    <Category("Projekt")>
    Public Property DropDownStyle As ComboBoxStyle
        Get
            Return cmbList.DropDownStyle
        End Get
        Set(value As ComboBoxStyle)
            If Not cmbList.DropDownStyle = ComboBoxStyle.Simple Then
                cmbList.DropDownStyle = value
            End If
        End Set
    End Property
#End Region

    'Data
    <Category("Projekt - Data")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> <Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))> <Localizable(True)> <MergableProperty(False)>
    Public ReadOnly Property Items As ComboBox.ObjectCollection
        Get
            Return cmbList.Items
        End Get
    End Property
    <Category("Projekt - Data")>
    <AttributeProvider(GetType(IListSource))> <RefreshProperties(RefreshProperties.Repaint)>
    Public Property DataSource As Object
        Get
            Return cmbList.DataSource
        End Get
        Set(value As Object)
            cmbList.DataSource = value
        End Set
    End Property
    <Category("Projekt - Data")>
    <Browsable(True)> <DefaultValue(AutoCompleteSource.None)> <EditorBrowsable(EditorBrowsableState.Always)>
    Public Property AutoCompleteSource As AutoCompleteSource
        Get
            Return cmbList.AutoCompleteSource
        End Get
        Set(value As AutoCompleteSource)
            cmbList.AutoCompleteSource = value
        End Set
    End Property
    <Category("Projekt - Data")>
    <Browsable(True)> <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> <Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))> <EditorBrowsable(EditorBrowsableState.Always)> <Localizable(True)>
    Public Property AutoCompleteCustomSource As AutoCompleteStringCollection
        Get
            Return cmbList.AutoCompleteCustomSource
        End Get
        Set(value As AutoCompleteStringCollection)
            cmbList.AutoCompleteCustomSource = value
        End Set
    End Property
    <Category("Projekt - Data")>
    <Browsable(True)> <DefaultValue(AutoCompleteMode.None)> <EditorBrowsable(EditorBrowsableState.Always)>
    Public Property AutoCompleteMode As AutoCompleteMode
        Get
            Return cmbList.AutoCompleteMode
        End Get
        Set(value As AutoCompleteMode)
            cmbList.AutoCompleteMode = value
        End Set
    End Property
    <Category("Projekt - Data")>
    <Bindable(True)> <Browsable(False)> <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SelectedItem As Object
        Get
            Return cmbList.SelectedItem
        End Get
        Set(value As Object)
            cmbList.SelectedIndex = value
        End Set
    End Property
    <Category("Projekt - Data")>
    <Browsable(False)> <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SelectedIndex As Integer
        Get
            Return cmbList.SelectedIndex
        End Get
        Set(value As Integer)
            cmbList.SelectedIndex = value
        End Set
    End Property

    'Events
    Public Event OnSelectedIndexChanged As EventHandler

    'Contructor
    Public Sub New()
        cmbList = New ComboBox()
        lblText = New Label()
        btnIcon = New Button()
        Me.SuspendLayout()

        'ComboBox: Dropdown list
        cmbList.BackColor = _listBackColor
        cmbList.Font = New Font(Me.Font.Name, 10.0F)
        cmbList.ForeColor = _listTextColor
        AddHandler cmbList.SelectedIndexChanged, New EventHandler(AddressOf ComboBox_SelectedIndexChanged) 'Default event
        AddHandler cmbList.TextChanged, New EventHandler(AddressOf ComboBox_TextChanged) 'Refresh text

        'Button: Icon
        btnIcon.Dock = DockStyle.Right
        btnIcon.FlatStyle = FlatStyle.Flat
        btnIcon.FlatAppearance.BorderSize = 0
        btnIcon.BackColor = _backColor
        btnIcon.Size = New Size(30, 30)
        btnIcon.Cursor = Cursors.Hand
        AddHandler btnIcon.Click, New EventHandler(AddressOf Icon_Click) 'Open dropdown list
        AddHandler btnIcon.Paint, New PaintEventHandler(AddressOf Icon_Paint) 'Draw icon

        'Label: Text
        lblText.Dock = DockStyle.Fill
        lblText.AutoSize = False
        lblText.BackColor = _backColor
        lblText.TextAlign = ContentAlignment.MiddleLeft
        lblText.Padding = New Padding(8, 0, 0, 0)
        lblText.Font = New Font(Me.Font.Name, 10.0F)
        '->Attach label events to user control event
        AddHandler lblText.Click, New EventHandler(AddressOf Surface_Click) 'Selecting combobox
        AddHandler lblText.MouseEnter, New EventHandler(AddressOf Surface_MouseEnter)
        AddHandler lblText.MouseLeave, New EventHandler(AddressOf Surface_MouseLeave)

        'User Control
        Me.Controls.Add(lblText)
        Me.Controls.Add(btnIcon)
        Me.Controls.Add(cmbList)
        Me.MinimumSize = New Size(200, 30)
        Me.Size = New Size(200, 30)
        Me.ForeColor = Color.DimGray
        Me.Padding = New Padding(_borderSize)
        MyBase.BackColor = _borderColor
        Me.ResumeLayout()
        AdjustComboBoxDimensions()

    End Sub

    'Private Methods
    Private Sub AdjustComboBoxDimensions()
        cmbList.Width = lblText.Width
        cmbList.Location = New Point With {
            .X = Me.Width - Me.Padding.Right - cmbList.Width,
            .Y = lblText.Bottom - cmbList.Height
        }

    End Sub


#Region "Events"
    'Default event
    Private Sub ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
        RaiseEvent OnSelectedIndexChanged(sender, e)


        'Refresh text
        lblText.Text = cmbList.Text

    End Sub
    Private Sub Surface_Click(sender As Object, e As EventArgs)
        'Select ComboBox
        cmbList.Select()
        If cmbList.DropDownStyle = ComboBoxStyle.DropDownList Then
            cmbList.DroppedDown = True 'Open dropdown
        End If

    End Sub

    Private Sub Surface_MouseLeave(sender As Object, e As EventArgs)
        Me.OnMouseLeave(e)
    End Sub

    Private Sub Surface_MouseEnter(sender As Object, e As EventArgs)
        Me.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        AdjustComboBoxDimensions()
    End Sub

    Private Sub Icon_Paint(sender As Object, e As PaintEventArgs)
        Dim iconWidth As Integer = 14
        Dim iconHeight As Integer = 6
        Dim rectIcon = New Rectangle((btnIcon.Width - iconWidth) / 2, (btnIcon.Height - iconHeight) / 2, iconWidth, iconHeight)
        Dim g As Graphics = e.Graphics

        'Drow downarrow
        Using path As New GraphicsPath
            Using pen As New Pen(_iconColor, 2)
                g.SmoothingMode = SmoothingMode.AntiAlias
                path.AddLine(rectIcon.X, rectIcon.Y, CSng(rectIcon.X + (iconWidth / 2)), rectIcon.Bottom)
                path.AddLine(CSng(rectIcon.X + (iconWidth / 2)), rectIcon.Bottom, rectIcon.Right, rectIcon.Y)
                g.DrawPath(pen, path)

            End Using
        End Using

    End Sub

    Private Sub Icon_Click(sender As Object, e As EventArgs)
        cmbList.Select()
        cmbList.DroppedDown = True 'Open dropdown

    End Sub

    Private Sub ComboBox_TextChanged(sender As Object, e As EventArgs)
        'Refresh text
        lblText.Text = cmbList.Text

    End Sub
#End Region


End Class
