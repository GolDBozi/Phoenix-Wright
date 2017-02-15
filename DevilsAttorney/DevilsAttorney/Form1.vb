Imports System.Data.OleDb
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class Form1
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'edw ginetai h apothukeysh tou pelath sto database
        On Error GoTo SaveErr
        ClientListBindingSource.EndEdit()
        ClientListTableAdapter.Update(ClientListDataSet.ClientList)
        MessageBox.Show("Προστέθηκε νέος πελάτης!")
        Button2.Visible = True
        Button5.Visible = True
SaveErr:
        Exit Sub
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'edw kanoume diagrafh 
        Dim con As OleDbConnection
        Dim com As OleDbCommand
        'edw prepei na valete to path sas gia ekei p einai h vash C:\Users\GolDBozi\Documents\Visual Studio 2015\Projects\DevilsAttorney\DevilsAttorney\bin\Debug\ClientList.accdb
        con = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= C:\Users\GolDBozi\Documents\Visual Studio 2015\Projects\DevilsAttorney\DevilsAttorney\bin\Debug\ClientList.accdb")
        com = New OleDbCommand("delete from ClientList where Επώνυμο =@sno", con)
        con.Open()
        com.Parameters.AddWithValue("@sno", TextBox2.Text)
        com.ExecuteNonQuery()
        MsgBox("Ένας πελάτης διαγράφηκε")
        con.Close()
        ClientListBindingSource.RemoveCurrent()


    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'kleinoyme thn efarmogh
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'TODO: This line of code loads data into the 'ClientListDataSet.ClientList' table. You can move, or remove it, as needed.
        Me.ClientListTableAdapter.Fill(Me.ClientListDataSet.ClientList)
        Dim toolTip1 As New ToolTip()
        toolTip1.AutoPopDelay = 5000
        toolTip1.InitialDelay = 1000
        toolTip1.ReshowDelay = 500
        toolTip1.ShowAlways = True
        toolTip1.SetToolTip(Me.Button5, "Κάντε κλικ στον πελάτη που θέλετε να διαγράψετε!")
        Dim toolTip2 As New ToolTip()
        toolTip2.AutoPopDelay = 5000
        toolTip2.InitialDelay = 1000
        toolTip2.ReshowDelay = 500
        toolTip2.ShowAlways = True
        toolTip2.SetToolTip(Me.Button4, "Κάντε κλικ όταν συμπληρώσετε όλα τα πεδία!")
        Dim toolTip3 As New ToolTip()
        toolTip3.AutoPopDelay = 5000
        toolTip3.InitialDelay = 1000
        toolTip3.ReshowDelay = 500
        toolTip3.ShowAlways = True
        toolTip3.SetToolTip(Me.Button2, "Κάντε κλικ για να κάνετε νέα καταχώρηση!")
        Label9.Text = String.Join(Environment.NewLine, Array.ConvertAll(Label9.Text.ToCharArray, Function(c) c.ToString))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'edw paei sthn prohgoumenh epilogh
        ClientListBindingSource.MovePrevious()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'edw patame gia nea kataxwrhsh
        ClientListBindingSource.AddNew()
        Button2.Visible = False
        Button4.Visible = True
        Button5.Visible = False

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'phgene sthn epomenh epilogh
        ClientListBindingSource.MoveNext()

    End Sub

    Protected Sub Encrypt(sender As Object, e As EventArgs)
        TextBox8.Text = Me.Encrypt(TextBox7.Text.Trim())
    End Sub

    Private Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox8.Text = Me.Encrypt(TextBox7.Text.Trim())
    End Sub

    Protected Sub Decrypt(sender As Object, e As EventArgs)
        TextBox8.Text = Me.Decrypt(TextBox8.Text.Trim())
    End Sub

    Private Function Decrypt(cipherText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Label8.Text = Me.Decrypt(TextBox8.Text.Trim())
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub
End Class
