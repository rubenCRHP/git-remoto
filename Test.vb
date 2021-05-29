Imports System
Imports System.IO
Imports MisClases.ES ' clase Leer

' Aplicación para trabajar con la clase CBanco y la jerarquía
' de clases derivadas de CCuenta
'
Public Class Test
  Public Shared Sub EscribirDatos(ByVal banco As CBanco, ByVal fich As String)
    Dim fcli As StreamWriter = Nothing
    Dim cliente As CCuenta
    Dim lista As CListaClientes = New CListaClientes(banco.Longitud())
    Try
      For i As Integer = 0 To banco.Longitud() - 1
        cliente = banco.Obtener(i)
        lista.Añadir(cliente.ObtenerNombre(), i)
      Next i
      ' Abrir el fichero para escribir. Se crea el flujo fcli;
      fcli = New StreamWriter(fich)
      lista.Escribir(fcli)
    Catch e As IOException
      Console.WriteLine(e.Message)
    Finally
      ' Cerrar el fichero
      If (Not fcli Is Nothing) Then fcli.Close()
    End Try
  End Sub

  ' Para la entrada de datos se utiliza Leer
  Public Shared Function LeerDatos(ByVal op As Integer) As CCuenta
    Dim obj As CCuenta = Nothing
    Dim nombre, cuenta As String
    Dim saldo, tipoi, mant As Double
    Console.Write("Nombre.................: ")
    nombre = Console.ReadLine()
    Console.Write("Cuenta.................: ")
    cuenta = Console.ReadLine()
    Console.Write("Saldo..................: ")
    saldo = Leer.datoDouble()
    Console.Write("Tipo de interés........: ")
    tipoi = Leer.datoDouble()
    If (op = 1) Then
      Console.Write("Mantenimiento..........: ")
      mant = Leer.datoDouble()
      obj = New CCuentaAhorro(nombre, cuenta, saldo, tipoi, mant)
    Else
      Dim transex As Integer
      Dim imptrans As Double
      Console.Write("Importe por transacción: ")
      imptrans = Leer.datoDouble()
      Console.Write("Transacciones exentas..: ")
      transex = Leer.datoInt()
      If (op = 2) Then
        obj = New CCuentaCorriente(nombre, cuenta, saldo, tipoi, imptrans, transex)
      Else
        obj = New CCuentaCorrienteConIn(nombre, cuenta, saldo, tipoi, imptrans, transex)
      End If
    End If
    Return obj
  End Function

  Public Shared Function Menú() As Integer
    Console.Write(Constants.vbLf + Constants.vbLf)
    Console.WriteLine("1.  Saldo")
    Console.WriteLine("2.  Buscar siguiente")
    Console.WriteLine("3.  Ingreso")
    Console.WriteLine("4.  Reintegro")
    Console.WriteLine("5.  Añadir")
    Console.WriteLine("6.  Eliminar")
    Console.WriteLine("7.  Mantenimiento")
    Console.WriteLine("8.  Copia de seguridad")
    Console.WriteLine("9.  Restaurar copia de seguridad")
    Console.WriteLine("10. Escribir")
    Console.WriteLine("11. Salir")
    Console.WriteLine()
    Console.Write("   Opción: ")
    Dim op As Integer
    Do
      op = Leer.datoInt()
    Loop While (op < 1 OrElse op > 11)
    Return op
  End Function

  Public Shared Sub Main(ByVal args As String())
    ' Crear un objeto banco vacío (con cero elementos)
    Dim banco As CBanco = New CBanco()
    Dim copiabanco As CBanco = Nothing ' para la copia de seguridad

    Dim opción As Integer = 0, pos As Integer = -1
    Dim cadenabuscar As String = Nothing
    Dim cuenta As String
    Dim cantidad As Double
    Dim eliminado As Boolean = False
    Dim fichero As String

    Do
      opción = Menú()
      Select Case opción
        Case 1 ' saldo
          Console.Write("Nombre total o parcial, o cuenta ")
          cadenabuscar = Console.ReadLine()
          pos = banco.Buscar(cadenabuscar, 0)
          If (pos = -1) Then
            If (banco.Longitud() <> 0) Then
              Console.WriteLine("búsqueda fallida")
            Else
              Console.WriteLine("no hay cuentas")
            End If
          Else
            Console.WriteLine(banco.Obtener(pos).ObtenerNombre())
            Console.WriteLine(banco.Obtener(pos).ObtenerCuenta())
            Console.WriteLine(banco.Obtener(pos).Saldo())
          End If
        Case 2 ' buscar siguiente
          pos = banco.Buscar(cadenabuscar, pos + 1)
          If (pos = -1) Then
            If (banco.Longitud() <> 0) Then
              Console.WriteLine("búsqueda fallida")
            Else
              Console.WriteLine("no hay cuentas")
            End If
          Else
            Console.WriteLine(banco.Obtener(pos).ObtenerNombre())
            Console.WriteLine(banco.Obtener(pos).ObtenerCuenta())
            Console.WriteLine(banco.Obtener(pos).Saldo())
          End If
        Case 3, 4 ' ingreso
          Console.Write("Cuenta: ")
          cuenta = Console.ReadLine()
          pos = banco.Buscar(cuenta, 0)
          If (pos = -1) Then
            If banco.Longitud() <> 0 Then
              Console.WriteLine("búsqueda fallida")
            Else
              Console.WriteLine("no hay cuentas")
            End If
          Else
            Console.Write("Cantidad: ")
            cantidad = Leer.datoDouble()
            If (opción = 3) Then
              banco.Obtener(pos).Ingreso(cantidad)
            Else
              banco.Obtener(pos).Reintegro(cantidad)
            End If
          End If
        Case 5 ' añadir
          Console.Write("Tipo de cuenta: 1-(CA), 2-(CC), 3-(CCI) ")
          Do
            opción = Leer.datoInt()
          Loop While (opción < 1 OrElse opción > 3)
          banco.Añadir(LeerDatos(opción))
        Case 6 ' eliminar
          Console.Write("Cuenta: ")
          cuenta = Console.ReadLine()
          eliminado = banco.Eliminar(cuenta)
          If (eliminado) Then
            Console.WriteLine("registro eliminado")
          Else
            If (banco.Longitud() <> 0) Then
              Console.WriteLine("cuenta no encontrada")
            Else
              Console.WriteLine("no hay cuentas")
            End If
          End If
        Case 7 ' mantenimiento
          For pos = 0 To banco.Longitud() - 1
            banco.Obtener(pos).Comisiones()
            banco.Obtener(pos).Intereses()
          Next pos
        Case 8 ' copia de seguridad
          If (banco.Longitud() = 0) Then Exit Select
          If (copiabanco Is Nothing) Then
            copiabanco = New CBanco(banco)
            'copiabanco = banco.Clonar();
            Console.WriteLine("copia realizada con éxito")
          Else
            Console.WriteLine("existe una copia, restaurarla")
          End If
        Case 9 ' restaurar copia de seguridad
          If (copiabanco Is Nothing) Then Exit Select
          banco = copiabanco
          Console.WriteLine("copia de seguridad restaurada")
          copiabanco = Nothing
        Case 10 ' escribir
          Try
            Console.Write("Fichero: ")
            fichero = Console.ReadLine()
            EscribirDatos(banco, fichero)
          Catch e As IOException
            Console.WriteLine(e.Message)
          End Try
        Case 11 ' salir
          If (Not copiabanco Is Nothing) Then copiabanco = Nothing
          banco = Nothing
      End Select
    Loop While (opción <> 11)
  End Sub
End Class
