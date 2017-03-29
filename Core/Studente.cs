using System;

namespace Core
{
    public class Studente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime Data_di_nascita { get; set; }
        public DateTime Data_di_iscrizione { get; set; }
        public int Classe { get; set; }
        public string Sezione { get; set; }
    }
}
