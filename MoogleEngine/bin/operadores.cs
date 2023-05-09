


using System.Data.SqlTypes;

using System.Threading;
using System.ComponentModel;
using System.Net.Http;
using System.Xml;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Collections.Specialized;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
namespace operadores
{
   public class operando
    {
        
        //encuentra los operadores y los desaparece 
      public static string desaparecer(string query)
    {
           string [] quitar = new string[4]{"!","^","*","~"};
             List<string> lista = new List <string>(){query};
            lista = query.Split(' ').ToList();

            for (int i = 0; i < quitar.Length ; i++)
             {
                for (int j = 0; j < query.Length; j++)
                {
                  query = query.Replace(quitar[i] ,String.Empty);
                
                }
             }
          return query;
    }

        //calcula cuantos caracteres hay entre dos palabras
    public static int distancia (List <string> frase , string documento)
    {
        
            int distance = subString( frase[0] , frase[1], documento);
            return distance;
    }

        //extrae un substring entre dos palabras
    public static int subString (string a ,string b ,string docum)
    {
            string resultado = " ";
            int contador = 0;
            if(docum.Contains(a) && docum.Contains(b))
            {
            int inicio = docum.IndexOf(a,0) + a.Length;
            int final = docum.IndexOf(b,inicio);
            resultado = docum.Substring(inicio ,final - inicio);
           
           for (int i = 0; i < resultado.Length; i++)
           {
            contador++;
           }
            }
            if(!docum.Contains(a) || !docum.Contains(b))
            {
                contador = docum.Length;
            }
       // Console.WriteLine(contador);
            return contador;
    }
        //me saca la palabra que debe incluir y quita el operador
    public static List <string> inclusion(string query)
    {
    
            char a = '^';
            string b = "^";
           string palabra = " " ;

             List <string> cosas = new List<string>();
            cosas = query.Split(' ').ToList();
             
            for (int i = 0; i < cosas.Count; i++)
            {
                 
                 if(!cosas[i].Contains(a))
                  cosas.Remove(cosas[i]);
            }
            for (int i = 0; i < cosas.Count; i++)
            {
                  cosas[i] = desaparecer(cosas[i]);
            }
             return cosas;
      }
    
    
    //quita el operador ! y te saca las palabras q lo contienen
    public static  List<string> quitar(string query)
    {
        char a = '!';
            string b = "!";
            
           string palabra = " " ;

             List <string> cosas = new List<string>();
            cosas = query.Split(' ').ToList();

            for (int i = 0; i < cosas.Count; i++)
            {
                
                if(!cosas[i].Contains(a))
                cosas.Remove(cosas[i]);
            }
            for (int i = 0; i < cosas.Count; i++)
            {
                cosas[i] = desaparecer(cosas[i]);
               // Console.WriteLine(cosas[i]);
            
            }
             return cosas;
    }
    //me saca la palabra que tiene el operador *
    public static List <string> asterisco(string query)
    {
            char a = '*';
            string b = "*";
           string palabra = " " ;
           int contador = 0;

             List <string> cosas = new List<string>();
            cosas = query.Split(' ').ToList();

            for (int i = 0; i < cosas.Count; i++)
            {
                if(!cosas[i].Contains(a))
                cosas.Remove(cosas[i]);
            }
           
             return cosas;
    }
        
        //encuentra las dos palabras en el que esta el operador  
    public static List <string> OPdistance(string query)
    {
            string a = "~";
            List<string> melani = new List<string>();
             string pal1 = "";
             string pal2 = "";
            melani = query.Split(' ').ToList();
            int index = 0;

            for (int i = 0; i < melani.Count; i++)
            {
                if(melani[i] == a )
                {
                 index = i;
                }
              
            } 
            List <string> palabras = new List<string>();
            if(index != 0);
            pal1 = melani[index - 1];
            palabras.Add(pal1);
            Console.WriteLine(pal1);
            pal2 = melani[index + 1];
            palabras.Add(pal2);
            Console.WriteLine(pal2);

          return palabras;
    }
       
       //almacena en un array la cantidad de * por palabras
    public static int [] prioridad(List <string> frase)
    {
            char a = '*';
             int [] contador  = new int[frase.Count] ;
             int cuenta = 0;
            for (int i = 0; i < frase.Count; i++)
            {
                for (int j = 0; j < frase[i].Length; j++)
                {
                    if(frase[i][j] == '*')
                    cuenta++;
                    contador[i] = cuenta;
                }
            }
            return contador;
    }
    //modifica el escore
    public static double modificar(string query,string documento ,double escore)
    {
       bool desaparecer ;
       bool aparecer ;
   
       double escore2 = escore;
            
            if(query.Contains("!"))
             {
                List <string> listado = new List<string>();
                listado = quitar(query);
        
               for (int i = 0; i < listado.Count; i++)
                {
                 desaparecer = saber(documento,listado[i]);
                if(desaparecer == true) escore2 = 0;
                }
                
             }
            if(query.Contains("^") && escore2 == 0)
            {
             List <string> lista = new List<string>();
            
             lista = inclusion(query);

              for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine(lista[i]);
                aparecer = saber(documento,lista[i]);
                if(aparecer == false) escore2 = 0;
            }
            }
                if(escore2 != 0)
                {
                     escore2 = valor(query,documento,escore);
                }

               // Console.WriteLine(escore);
        return escore2;
    }
            public static double valor(string query ,string documento , double escore)
        {
            double escore1 = escore; 
            double escore2 = escore;
        //proceso el escore segun el  operador *
        if(query.Contains("*"))
        {
        List <string> frase = asterisco(query);

        int [] contadores = prioridad(frase);
       // Console.WriteLine(String.Join(' ',contadores));
        for (int i = 0; i < frase.Count; i++)
        {       frase[i] = desaparecer(frase[i]);
            if(documento.Contains(frase[i]))
            escore1 += Math.Pow(escore,contadores[i]*contadores[i]);
      //Console.WriteLine(escore1);

        }
        }
        if(query.Contains("~"))
        {
        //proceso el escore segun el operador de distancia 
        List <string> palabras = OPdistance(query);

        int dista = distancia(palabras,documento);
        // Console.WriteLine(dista);
        escore2 = escore/dista + 1;
        Console.WriteLine(escore2);
        }
        if(escore1 != 0 && escore2 != 0)
        escore = escore1 + escore2;
        
         return escore;
        }
    static bool saber(string docuemnto ,string palabra)
    {
        if(docuemnto.Contains(palabra))
        return true;
        else 
        {
        return false;
        }
    } 
}

}
