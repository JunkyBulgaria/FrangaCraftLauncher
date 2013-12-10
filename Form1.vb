Imports System.IO
Imports System.IO.Compression

Public Class Launcher
    Dim InstallPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\.FrangaCraft"
    Dim closeLauncher As Boolean = False

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        System.Diagnostics.Process.Start("http://mc.franga2000.com")
    End Sub

    Private Sub PremLogin_Click(sender As Object, e As EventArgs) Handles PremLogin.Click
        If My.Computer.FileSystem.FileExists(InstallPath & "\version") Then
            'If the modpack is installed

            Dim WinUser As String = System.Environment.UserName
            Dim webClient As New System.Net.WebClient
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
                My.Computer.FileSystem.WriteAllText("./run.bat", run, True)
                Shell(run, vbNormalFocus)
                If closeLauncher Then

                    Me.Close()
                End If
            Else
                MsgBox("Bad login!", , "Error")
            End If

        Else

            'If the modpack isn't installed
            If MsgBox("The modpack isn't installed. Nothing you can do about it (yet) :P", MsgBoxStyle.YesNo, "Error") = MsgBoxResult.Yes Then
                'Install the modpack
InstallPack:
                If My.Computer.FileSystem.FileExists(InstallPath) Then

                    ZipFile.ExtractToDirectory(InstallPath & "\download.zip", InstallPath)
                Else
                    My.Computer.FileSystem.CreateDirectory(InstallPath)
                    GoTo InstallPack
                End If
            End If
        End If
    End Sub

    Private Sub PremPass_KeyDown(sender As Object, e As KeyEventArgs) Handles PremPass.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PremLogin_Click(sender, New System.EventArgs())
        End If
    End Sub
End Class
