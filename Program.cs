using System;
using System.Collections.Generic;
using System.IO;
#nullable disable

using System.Text.Json;

namespace books
{
    public class GuestBook
    {
        private string filename = @"guestbook.json"; //Lagring av inlägg
        private List<Book> books = new List<Book>(); //Lista alla lagrade element 
        
        public GuestBook(){ 
            if(File.Exists(@"guestbook.json")==true){ // Läs in filen om den finns
                string jsonString = File.ReadAllText(filename);
                books = JsonSerializer.Deserialize<List<Book>>(jsonString);
            }
        }
        // Metod för att lägga till ett inlägg
        public Book addToBook(Book book){
            books.Add(book); //Metod ADD för at lägga till
            marshal(); // Serialisering av data        
            return book; //Returnera objektet
        }
        
        //Metod för att ta bort inlägg, tar index som argument
        public int delBook(int index){
            books.RemoveAt(index); // Anger vilket objekt som ska raderas
            marshal(); // Serialisering av data
            return index;
        }

        //Returnerar hela arrayen
        public List<Book> getBooks(){
            return books;
        }


        private void marshal()
        {
            // Serialiera alla objekt och spara till json-filen
            var jsonString = JsonSerializer.Serialize(books);
            File.WriteAllText(filename, jsonString);
        }
    }

 //Klass som innehåller strängar för variabler namn och inlägg
    public class Book
    {
        private string name; 
        private string post;          
        public string Name
        {
            set {this.name = value;} // set värde
            get {return this.name;} // hämtar ut värdet
        }
        public string Post {
            set {this.post = value;}
            get {return this.post;}
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            //Instansierar en klass, ger tillgång till add, delete
            GuestBook guestbook = new GuestBook();
            int i=0;
            //Slingan körs tills X anges för att avsluta (true)
            while(true){
                Console.Clear();Console.CursorVisible = false;
                Console.WriteLine("SARAS GÄSTBOK\n\n");
                Console.WriteLine("1. Skapa inlägg");
                Console.WriteLine("2. Ta bort inlägg\n");
                Console.WriteLine("X. Avsluta\n");

                //Loopa igenom arrayen och skriv ut index + namn + inlägg
                i=0;
                foreach(Book book in guestbook.getBooks()){
                    Console.WriteLine("[" + i++ + "] " + book.Name + " - " + book.Post);
                    
                }

                int inp = (int) Console.ReadKey(true).Key;
                
                switch (inp) {

                    case '1':
                    
                        
                        Console.CursorVisible = true; 
                        // Hämta och lagra namn
                        Console.Write("Ange Namn: ");
                        string theName = Console.ReadLine();
                        Book obj = new Book();
                        obj.Name = theName;
            
                        // Hämta och lagra inlägget
                        Console.Write("Ditt inlägg: ");
                        string thePost = Console.ReadLine();
                        obj.Post = thePost;
 
                        //Kontroll om längd på namn eller inlägg är mindre än 1, om detta inte stämmer lagras namn och inlägg
                        if(theName.Length < 1 || thePost.Length < 1) {
			            Console.Write("Du behöver ange både namn och ett inlägg!");
                        Console.ReadKey();
                        break;
                       
                          } else {
                        
                        guestbook.addToBook(obj);
                         Console.Clear();
                         }
                        
                       break;
      

                    case '2': 
                    
                        Console.CursorVisible = true;
                        //Hämtar vilket index som ska raderas
                        Console.Write("Ange index att radera: ");
                        string index = Console.ReadLine();
                        if (index == "") {
                              Console.Clear();
                        }else {
                        guestbook.delBook(Convert.ToInt32(index)); //GÖr om strängen till ett heltal
                        Console.Clear();
                        }
                        break;

                        
                    case 88: 
                        Environment.Exit(0); // Avsluta applikationen
                        break;
                }
                
 
            }
            
        }
    }
}
