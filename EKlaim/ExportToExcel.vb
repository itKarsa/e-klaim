Imports System.IO
Module ExportToExcel
    Public Sub ExToExcel(ByVal DGV As DataGridView, ByVal DGVDOK As DataGridView, ByVal FlNm As String)
        Dim fs As New StreamWriter(FlNm, False)
        With fs
            .WriteLine("<?xml version=""1.0""?>")
            .WriteLine("<?mso-application progid=""Excel.Sheet""?>")
            .WriteLine("<Workbook 
                            xmlns=""urn:schemas-Microsoft-com:office:spreadsheet""
                            xmlns:o=""urn:schemas-Microsoft-com:office:office""
                            xmlns:x=""urn:schemas-Microsoft-com:office:excel""
                            xmlns:ss=""urn:schemas-Microsoft-com:office:spreadsheet""
                            xmlns:html=""http://www.w3.org/TR/REC-html40"">")
            .WriteLine("    <Styles>")
            .WriteLine("        <Style ss:ID=""Default"">")
            .WriteLine("            <Alignment ss:Vertical=""Bottom""/>")
            .WriteLine("            <Borders/>")
            .WriteLine("            <Font ss:FontName=""Calibri""/>") 'SET FONT
            .WriteLine("        </Style>")
            .WriteLine("    </Styles>")
            If DGV.Name = "Ranap" Then
                .WriteLine("    <Worksheet ss:Name=""Ranap"">") 'SET NAMA SHEET
                .WriteLine("        <Table>")
                '.WriteLine("            <Column ss:Width=""27.75""/>") 'No
                '.WriteLine("            <Column ss:Width=""93""/>") 'NIK
                '.WriteLine("            <Column ss:Width=""84""/>") 'Nama
                '.WriteLine("            <Column ss:Width=""100""/>") 'Alamat
                '.WriteLine("            <Column ss:Width=""84""/>") 'Telp
            ElseIf DGV.Name = "Rajal" Then
                .WriteLine("    <Worksheet ss:Name=""Rajal"">") 'SET NAMA SHEET
                .WriteLine("        <Table>")
            End If
            'AUTO SET HEADER
            .WriteLine("            <Row>")
            For i As Integer = 0 To DGV.Columns.Count - 1 'SET HEADER
                Application.DoEvents()
                .WriteLine("            <Cell>")
                .WriteLine("                <Data ss:Type=""String"">{0}</Data>", DGV.Columns.Item(i).HeaderText)
                .WriteLine("            </Cell>")
            Next
            .WriteLine("            </Row>")
            For intRow As Integer = 0 To DGV.RowCount - 1
                Application.DoEvents()
                .WriteLine("        <Row>")
                For intCol As Integer = 0 To DGV.Columns.Count - 1
                    Application.DoEvents()
                    .WriteLine("        <Cell>")
                    .WriteLine("            <Data ss:Type=""String"">{0}</Data>", DGV.Item(intCol, intRow).Value.ToString)
                    .WriteLine("        </Cell>")
                Next
                .WriteLine("        </Row>")
            Next
            .WriteLine("        </Table>")
            .WriteLine("    </Worksheet>")
            .WriteLine("    <Worksheet ss:Name=""Dokter"">") 'SET NAMA SHEET
            .WriteLine("        <Table>")
            'AUTO SET HEADER
            .WriteLine("            <Row>")
            For i As Integer = 0 To DGVDOK.Columns.Count - 1 'SET HEADER
                Application.DoEvents()
                .WriteLine("            <Cell>")
                .WriteLine("                <Data ss:Type=""String"">{0}</Data>", DGVDOK.Columns.Item(i).HeaderText)
                .WriteLine("            </Cell>")
            Next
            .WriteLine("            </Row>")
            For intRow As Integer = 0 To DGVDOK.RowCount - 1
                Application.DoEvents()
                .WriteLine("        <Row>")
                For intCol As Integer = 0 To DGVDOK.Columns.Count - 1
                    Application.DoEvents()
                    .WriteLine("        <Cell>")
                    .WriteLine("            <Data ss:Type=""String"">{0}</Data>", DGVDOK.Item(intCol, intRow).Value.ToString)
                    .WriteLine("        </Cell>")
                Next
                .WriteLine("        </Row>")
            Next
            .WriteLine("        </Table>")
            .WriteLine("    </Worksheet>")
            .WriteLine("</Workbook>")
            .Close()
        End With
    End Sub
End Module
