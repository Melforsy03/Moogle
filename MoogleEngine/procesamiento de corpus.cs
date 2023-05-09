
using System.Reflection;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Runtime.InteropServices.ComTypes;

using System.ComponentModel;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;


namespace Name
{

  public  class moggle
 {
  public static string [] textos = quitar();
   public static string [] ReadFolder()//escanear carpetas
  {
    string[] filas = Directory.GetFiles(@"C:\Users\fosythe´s family\Desktop\moogle melani forsythe", "*.txt", SearchOption.AllDirectories);
   
    return filas;
  }
  //lee dentro de las carpetas y subcarpetas
    public static string [] ReadFiles()
  {
    Console.WriteLine("leyendo documentos....");

    string [] files = ReadFolder();
  
    string [] contenedor = new string[files.Length];


      for (int i = 0; i < files.Length; i++)
      {
         string contenido = " ";
         //leer archivos
         StreamReader reader = new StreamReader(files[i]);

         while (!reader.EndOfStream)
         {
            //guardo el contenido del archivo
            contenido += reader.ReadLine() + " ";
         }
            contenedor[i] = contenido;

         

         reader.Close();
      }

  
      return contenedor;
  }
    //lee los nombres de los documentos
  public static string [] nombres()
  {
      string [] filas = ReadFolder();
      
      string [] direccion = new string [filas.Length];

      string [] name = new string [filas.Length];

      for (int i = 0; i < filas.Length; i++)
      {
        direccion[i] = filas[i];
        name[i] = Path.GetFileName(direccion[i]);
      }
     return name ;
  }
    //eliminar caracteres raros ,signos de puntuacion y mayusculas

    public static string [] quitar ()
    {
      //nota para mi:mejora esto para disminuir el tiempo de busqueda
      string [] texto = ReadFiles();
      string [] remover = new string [] {"@",",",".",";","&","$","#","%","+","=","?","`","!","~","^","*"};
      char a = 'á';
      char e = 'é';
      char ini = 'í';
      char o = 'ó';
      char u = 'ú';
      for (int i = 0; i < remover.Length; i++)
      {

        for (int j = 0; j < texto.Length; j++)
        {
          texto[j] = texto[j].Replace(remover[i],string.Empty);
          texto[j] = texto[j].ToLower();
          texto[j] = texto[j].Replace(a,'a');
          texto[j] = texto[j].Replace(e ,'e');
          texto[j] = texto[j].Replace(ini,'i');
          texto[j] = texto[j].Replace(o,'o');
          texto[j] = texto[j].Replace(u,'u');
          
      }
      }
      return texto;
    }
    public static string quitado(string query)
    {
      string texto = query.ToLower();
      string [] remover = new string [] {"@",",",".",";","&","$","#","!","%","+","=","^","?","`","~","!","*",};
      char a = 'á';
      char e = 'é';
      char ini = 'í';
      char o = 'ó';
      char u = 'ú';
      for (int i = 0; i < remover.Length; i++)
      {
          texto= texto.Replace(remover[i],String.Empty);
          texto = texto.Replace(a,'a');
          texto = texto.Replace(e ,'e');
          texto = texto.Replace(ini,'i');
          texto = texto.Replace(o,'o');
          texto = texto.Replace(u,'u');
      }
      
      
      return texto;
    }        
 }
}
    
      