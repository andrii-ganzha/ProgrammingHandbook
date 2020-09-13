Imports System
Imports System.IO
Imports SautinSoft.Document

Module Sample

    Sub Main()
        UnitConversion()
    End Sub

    ''' <summary>
    ''' This sample shows how to work with UnitConversion.
    ''' </summary>
    Public Sub UnitConversion()
        For Each unit As LengthUnit In System.Enum.GetValues(GetType(LengthUnit))
            Dim s As String = String.Format("1 point = {0} {1}", LengthUnitConverter.Convert(1, LengthUnit.Point, unit), unit.ToString().ToLowerInvariant())

            Console.WriteLine(s)
        Next unit

        Console.ReadLine()
    End Sub

End Module
