Imports System.CodeDom.Compiler
Imports System.Text.RegularExpressions
Imports Operational

Public Class Scripting
    Public Shared Function CompileReportScriptObj(ByVal Source As String, ByRef ErrorDT As DataTable) As ITest
        Dim reference As String
        Dim results As CompilerResults
        Dim _compiledScript As ITest = Nothing

        Source = AddFullCode(Source)

        ErrorDT = New DataTable
        reference = System.IO.Path.GetDirectoryName(GetType(Scripting).Assembly.Location)
        If Not reference.EndsWith("\") Then reference &= "\"
        'reference &= "interfaces.dll"
        reference &= "Operational.dll"

        results = CompileScript(Source)

        If results.Errors.Count() = 0 Then
            _compiledScript = DirectCast(FindInterface(results.CompiledAssembly, "ITest"), ITest)
            Return _compiledScript
        Else
            Dim err As CompilerError
            Dim ErrorDR As DataRow
            ErrorDT.Columns.Add("Line", Type.GetType("System.String"))
            ErrorDT.Columns.Add("Error", Type.GetType("System.String"))
            For Each err In results.Errors
                ErrorDR = ErrorDT.NewRow()
                ErrorDR("Error") = err.ErrorText
                ErrorDR("Line") = err.Line.ToString
                ErrorDT.Rows.Add(ErrorDR)
            Next
            Return Nothing
        End If

    End Function

    Private Shared Function CompileScript(ByVal Source As String) As CompilerResults
        'Dim provider As CodeDomProvider = New Microsoft.VisualBasic.VBCodeProvider
        'Dim compiler As ICodeCompiler = provider.CreateCompiler()
        Dim provider As CodeDomProvider = New Microsoft.VisualBasic.VBCodeProvider
        'Dim compiler As ICodeCompiler = provider.CreateCompiler()

        Dim params As New CompilerParameters
        Dim results As CompilerResults
        Dim Interface_reference As String

        'Find reference
        Interface_reference = System.IO.Path.GetDirectoryName(GetType(Scripting).Assembly.Location)
        If Not Interface_reference.EndsWith("\") Then Interface_reference &= "\"
        ' Interface_reference &= "interfaces.dll"
        Interface_reference &= "Operational.dll"

        'Configure parameters
        With params
            .GenerateExecutable = False
            .GenerateInMemory = True
            .IncludeDebugInformation = False
            .ReferencedAssemblies.Add("System.Windows.Forms.dll")
            .ReferencedAssemblies.Add("System.Data.dll")
            .ReferencedAssemblies.Add("System.XML.dll")
            .ReferencedAssemblies.Add("System.dll")
            .ReferencedAssemblies.Add(Interface_reference)
        End With

        'Compile
        Try
            results = provider.CompileAssemblyFromSource(params, Source)
            'results = compiler.CompileAssemblyFromSource(params, Source)

        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & Source, MsgBoxStyle.Information)
            results = Nothing
        End Try
        Return results
    End Function

    Private Shared Function FindInterface(ByVal DLL As Reflection.Assembly, ByVal InterfaceName As String) As Object
        Dim t As Type

        'Loop through types looking for one that implements the given interface
        For Each t In DLL.GetTypes()
            If Not (t.GetInterface(InterfaceName, True) Is Nothing) Then
                Return DLL.CreateInstance(t.FullName)
            End If
        Next

        Return Nothing
    End Function
    Private Shared Function AddFullCode(ByVal ReportSource As String) As String
        Dim src As String = ""
        ' ''src &= "Imports System.Configuration" & vbCrLf
        ' ''src &= "Imports System.Data.Common" & vbCrLf
        ' ''src &= "Imports Microsoft.VisualBasic" & vbCrLf
        '' ''src &= "Imports Interfaces" & vbCrLf
        ' ''src &= "Imports Operational" & vbCrLf
        src &= "Imports System.Configuration" & vbCrLf
        src &= "Imports System.Data" & vbCrLf
        src &= "Imports Microsoft.VisualBasic" & vbCrLf
        'src &= "Imports Interfaces" & vbCrLf
        src &= "Imports Operational" & vbCrLf


        src &= "Public Class Test" & vbCrLf
        src &= "    Implements ITest" & vbCrLf
        src &= "    Public Function GenerateReportDocument(ByVal Customer As String, ByVal GoldQuality As String, ByVal ItemCategory As String, ByVal ItemName As String, ByVal GemsCategory As String, ByVal Staff As String, ByVal FromDate As Date, ByVal ToDate As Date)  As DataSet Implements ITest.GenerateReportDocument " & vbCrLf
        src &= ReportSource & vbCrLf
        src &= "    End Function" & vbCrLf
        src &= "End Class" & vbCrLf

        'src &= "End Namespace" & vbCrLf
        'src &= " Dim dtName as string" & vbCrLf
        'src &= " Functions.SelectData(" & ReportSource & ",dtName,Customer,GoldQuality,ItemCategory,GemsCategory,Staff,FromDate,ToDate)" & vbCrLf
        Return src
    End Function


End Class
