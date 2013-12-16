Imports System.IO
Imports System.IO.Compression
Imports System.Net

Public Class Launcher
    Public Function GetSettingItem(ByVal file As String, ByVal identifier As String)
        Dim sr As New IO.StreamReader(file) : Dim result As String = ""
        Do While (sr.Peek <> -1)
            Dim line As String = sr.ReadLine
            If line.ToLower.StartsWith(identifier.ToLower & ":") Then
                result = line.Substring(identifier.Length + 2)
            End If
        Loop
        Return result
    End Function
    Dim WithEvents webClient As New System.Net.WebClient
    Dim InstallPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\.FrangaCraft"
    Dim closeLauncher As Boolean = False
    Dim latestMP As String = webClient.DownloadString("https://dl.dropboxusercontent.com/u/23206778/FrangaCraft/FrangaCraft%20Launcher/Data/latest")

    Private Sub Launcher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MsgBox(latestMP, , "latestMp")
    End Sub

    Private Sub Download()
        ProgressBar1.Show()
        webClient.DownloadFileAsync(New Uri("https://dl.dropboxusercontent.com/u/23206778/FrangaCraft/FrangaCraft%20Launcher/Data/" & latestMP & ".zip"), InstallPath & " \download.zip")
    End Sub
    Private Sub webClient_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles webClient.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub
    Private Sub webClient_DownloadFileCompleted() Handles webClient.DownloadFileCompleted
        ZipFile.ExtractToDirectory(InstallPath & "\download.zip", InstallPath)
        ProgressBar1.Value = 0
        ProgressBar1.Hide()
        My.Computer.FileSystem.DeleteFile(InstallPath & "\download.zip")
        MsgBox("The modpack was succesfully downloaded." & vbNewLine & "You can start playing now!", , "Download completed!")
    End Sub
    Private Sub Update()
        Dim currentMP As String = My.Computer.FileSystem.ReadAllText(InstallPath & "\version")
        ProgressBar1.Show()
        webClient.DownloadFileAsync(New Uri("https://dl.dropboxusercontent.com/u/23206778/FrangaCraft/FrangaCraft%20Launcher/Data/" & currentMP & ".zip"), InstallPath & " \download.zip")
        ZipFile.ExtractToDirectory(InstallPath & "\download.zip", InstallPath)
    End Sub
    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        System.Diagnostics.Process.Start("http://mc.franga2000.com")
    End Sub

    Private Sub PremLogin_Click(sender As Object, e As EventArgs) Handles PremLogin.Click
StartPrem:
        If My.Computer.FileSystem.DirectoryExists(InstallPath) Then
            If File.Exists(InstallPath & "\version") Then
                Dim currentMP As String = My.Computer.FileSystem.ReadAllText(InstallPath & "\version")
                If Not (Convert.ToInt32(currentMP) < Convert.ToInt32(latestMP)) Then
                    Dim WinUser As String = System.Environment.UserName
                    Dim result As String = webClient.DownloadString("http://login.minecraft.net/?user=" & PremUser.Text() & "&password=" & PremPass.Text & "&version=13")
                    If Not (result = "Bad login") Then
                        MsgBox(result, , "result")
                        Dim result_token As Array = Split(result, ":", 4)
                        Dim result_mcUser As Array = Split(result, ":", 5)
                        Dim token As String = result_token(3)
                        Dim mcUser As String = result_mcUser(2)
                        MsgBox(token, , "token")
                        MsgBox(mcUser, , "mcUser")
                        Dim McWidth As String = "1280"
                        Dim McHeight As String = "720"
                        Dim RAM As String = "1G"
                        Dim run As String = InstallPath & "\1STUFF\direct.bat " & InstallPath & " " & mcUser & " " & token & " " & RAM
                        'My.Computer.FileSystem.WriteAllText("./run.bat", run, True)
                        Shell(run, vbNormalFocus)
                        If closeLauncher Then

                            Me.Close()
                        End If
                    Else
                        MsgBox("Bad login!", , "Error")
                    End If

                Else

                    'If the modpack isn't installed
                    If MsgBox("You don't have the latest version of the modpack! Want to update?", MsgBoxStyle.YesNo, "Error") = MsgBoxResult.Yes Then
                        Update()
                    End If
                End If
            Else
                If MsgBox("You don't have the modpack! Want to install it?", MsgBoxStyle.YesNo, "Error") = MsgBoxResult.Yes Then
                    Download()
                End If
            End If
        Else
            My.Computer.FileSystem.CreateDirectory(InstallPath)
            GoTo StartPrem
        End If
    End Sub

    Private Sub PremPass_KeyDown(sender As Object, e As KeyEventArgs) Handles PremPass.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PremLogin_Click(sender, New System.EventArgs())
        End If
    End Sub
    Private Sub TxtBox_CrackedUser_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBox_CrackedUser.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Button3_Click(sender, New System.EventArgs())
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If My.Computer.FileSystem.DirectoryExists(InstallPath) Then
            If File.Exists(InstallPath & "\version") Then
                Dim currentMP As String = My.Computer.FileSystem.ReadAllText(InstallPath & "\version")
                If Not (Convert.ToInt32(currentMP) < Convert.ToInt32(latestMP)) Then
                    Dim token = "FrangaCraft_Cracked_Launcher"
                    Dim mcUser As String = TxtBox_CrackedUser.Text()
                    Dim McWidth As String = "1280"
                    Dim McHeight As String = "720"
                    Dim RAM As String = "1G"
                    Dim run As String = InstallPath & "\1STUFF\direct.bat " & InstallPath & " " & mcUser & " " & token & " " & RAM
                    'My.Computer.FileSystem.WriteAllText("./run.bat", run, True)
                    Shell(run, vbNormalFocus)
                    If closeLauncher Then

                        Me.Close()
                    End If
                Else 'If you don't have the latest modpack

                    If MsgBox("You don't have the latest version of the modpack! Want to update?", MsgBoxStyle.YesNo, "Error") = MsgBoxResult.Yes Then
                        Update()
                    End If
                End If
            Else 'If you don't have the modpack at all
                If MsgBox("You don't have the modpack! Want to install it?", MsgBoxStyle.YesNo, "Error") = MsgBoxResult.Yes Then
                    Download()
                End If
            End If
        Else
            My.Computer.FileSystem.CreateDirectory(InstallPath)
        End If
    End Sub

    Private Sub Btn_save_settings_Click(sender As Object, e As EventArgs) Handles Btn_save_settings.Click
        'GetSettingItem("", "")
    End Sub



    'Random buttons
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_Website.Click
        System.Diagnostics.Process.Start("http://mc.franga2000.com")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Btn_GitHub.Click
        System.Diagnostics.Process.Start("http://mc.franga2000.com")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Btn_MP_info.Click
        System.Diagnostics.Process.Start("http://mc.franga2000.com")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Btn_credits.Click
        System.Diagnostics.Process.Start("http://mc.franga2000.com")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Btn_about.Click
        System.Diagnostics.Process.Start("http://mc.franga2000.com")
    End Sub
End Class
