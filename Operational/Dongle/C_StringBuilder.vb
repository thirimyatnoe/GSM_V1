' Copyright (c) George "B" Gilbert, 2006-2008
Imports System.Text
Public Class C_StringBuilder
#Region " Members "

    Private _sb As New StringBuilder

#End Region
#Region " Constructors "
#Region " ... No Parameters "
    Public Sub New()
        '---------------------------------------------------------------------------------
        '     Date          Developer                      Code Change       
        '  ---------- -------------------- -----------------------------------------------
        '  12/15/2007 G Gilbert            Original code
        '---------------------------------------------------------------------------------

        _sb.Length = 0

    End Sub
#End Region
#Region " ... With Initial String "
    Public Sub New(ByVal InitialString As String)
        '---------------------------------------------------------------------------------
        '     Date          Developer                      Code Change       
        '  ---------- -------------------- -----------------------------------------------
        '  12/15/2007 G Gilbert            Original code
        '---------------------------------------------------------------------------------

        _sb.Length = 0
        _sb.Append(InitialString)

    End Sub
#End Region
#End Region
#Region " Methods "
#Region " ... AppendText "
    Public Sub AppendText(ByVal Text As String)
        '---------------------------------------------------------------------------------
        ' Append the passed string to the stringbuilder object
        '---------------------------------------------------------------------------------
        '     Date          Developer                      Code Change
        '  ---------- -------------------- -----------------------------------------------
        '  12/15/2007 G Gilbert            Original code
        '---------------------------------------------------------------------------------

        _sb.Append(Text)

    End Sub
#End Region
#Region " ... ClearText "
    Public Sub ClearText()
        '---------------------------------------------------------------------------------
        ' Reset the stringbuilder object
        '---------------------------------------------------------------------------------
        '     Date          Developer                      Code Change
        '  ---------- -------------------- -----------------------------------------------
        '  12/15/2007 G Gilbert            Original code
        '---------------------------------------------------------------------------------

        _sb.Length = 0

    End Sub
#End Region
#End Region
#Region " Properties "
#Region " ... FullText "
    Public ReadOnly Property FullText() As String
        '---------------------------------------------------------------------------------
        ' Get the full contents of the stringbuilder object
        '---------------------------------------------------------------------------------
        '     Date          Developer                      Code Change       
        '  ---------- -------------------- -----------------------------------------------
        '  12/15/2007 G Gilbert            Original code
        '---------------------------------------------------------------------------------

        Get
            FullText = _sb.ToString
        End Get

    End Property
#End Region
#End Region
End Class
