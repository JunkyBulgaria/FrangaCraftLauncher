Public Class Launcher

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        System.Diagnostics.Process.Start("http://mc.franga2000.com")
    End Sub

    Private Sub PremLogin_Click(sender As Object, e As EventArgs) Handles PremLogin.Click
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
            Dim run As String = """C:\Program Files\Java\jre7\bin\javaw.exe"" -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump -Xmx1G -Djava.library.path=C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\versions\LiteLoader1.6.2\LiteLoader1.6.2-natives -cp C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\net\minecraftforge\minecraftforge\9.10.1.871\minecraftforge-9.10.1.871.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\org\ow2\asm\asm-all\4.1\asm-all-4.1.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\com\mumfrey\liteloader\1.6.2\liteloader-1.6.2.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\net\minecraft\launchwrapper\1.3\launchwrapper-1.3.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\lzma\lzma\0.0.1\lzma-0.0.1.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\net\sf\jopt-simple\jopt-simple\4.5\jopt-simple-4.5.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\com\paulscode\codecjorbis\20101023\codecjorbis-20101023.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\com\paulscode\codecwav\20101023\codecwav-20101023.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\com\paulscode\libraryjavasound\20101123\libraryjavasound-20101123.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\com\paulscode\librarylwjglopenal\20100824\librarylwjglopenal-20100824.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\com\paulscode\soundsystem\20120107\soundsystem-20120107.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\argo\argo\2.25_fixed\argo-2.25_fixed.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\org\bouncycastle\bcprov-jdk15on\1.47\bcprov-jdk15on-1.47.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\com\google\guava\guava\14.0\guava-14.0.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\org\apache\commons\commons-lang3\3.1\commons-lang3-3.1.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\commons-io\commons-io\2.4\commons-io-2.4.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\net\java\jinput\jinput\2.0.5\jinput-2.0.5.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\net\java\jutils\jutils\1.0.0\jutils-1.0.0.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\com\google\code\gson\gson\2.2.2\gson-2.2.2.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\org\lwjgl\lwjgl\lwjgl\2.9.0\lwjgl-2.9.0.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\libraries\org\lwjgl\lwjgl\lwjgl_util\2.9.0\lwjgl_util-2.9.0.jar;C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\versions\LiteLoader1.6.2\LiteLoader1.6.2.jar net.minecraft.launchwrapper.Launch --username " & WinUser & " --session token:14ea672c0dbf4876af27c2a21ae90a04:e2406e13de45432b87fa8f3b530e5584 --gameDir C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft --assetsDir C:\Users\" & WinUser & "\Documents\Dev\data\.minecraft\assets --tweakClass com.mumfrey.liteloader.launch.LiteLoaderTweaker --cascadedTweaks cpw.mods.fml.common.launcher.FMLTweaker --width " & McWidth & " --height " & McHeight & "", vbNormalFocus
            Shell(run)
        Else
            MsgBox("Bad login!", , "Error")
        End If
    End Sub

    Private Sub PremPass_KeyDown(sender As Object, e As KeyEventArgs) Handles PremPass.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PremLogin_Click(sender, New System.EventArgs())
        End If
    End Sub
End Class
