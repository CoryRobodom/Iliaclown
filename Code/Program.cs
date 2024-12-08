using System;
using System.Linq;
using System.Collections;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace World
{
    internal class NewMailEventArgs:EventArgs
    {
        public NewMailEventArgs(string from, TypeOfMail typeOf, string mail){
            From = from;
            typeOfMail = typeOf;
            Mail = mail;
        }
        private readonly string From;
        private readonly TypeOfMail typeOfMail;
        private readonly string Mail;
        public string GetFrom
        {
            get
            {
                return From;
            }
        }
        public string GetMail
        {
            get
            {
                return Mail;
            }
        }
        public TypeOfMail GetTypeOfMail
        {
            get
            {
                return typeOfMail;
            }
        }
        public enum TypeOfMail{
            Mail,
            Invite,
            Advertisement
        }
        
    }
    internal class Person
    {
        private string From;
        private string Mail;
        private String name;
        public String Name{
        get{ return name;}
        set{if(value.Length >= 2) name = value;}
        }
        public int age;
        
        private NewMailEventArgs.TypeOfMail type;
        public Person(Company company){
            company.NewMail += Msg;
        }
        public void Msg(Object sender, NewMailEventArgs e)
        {
            From = e.GetFrom;
            Mail = e.GetMail;
            type = e.GetTypeOfMail;
        }
        public void UnRegist(Company company)
        {
            company.NewMail -= Msg;
        }
    }
    internal class Company
    {
        public event EventHandler<NewMailEventArgs> NewMail;
        protected virtual void OnNewMail(NewMailEventArgs e){
            EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);
            if(temp != null) temp(this, e);
        }
        public void CreateNewMail(string from, NewMailEventArgs.TypeOfMail dadwa , string muil){
            NewMailEventArgs e = new NewMailEventArgs(from, dadwa, muil);
            OnNewMail(e);
        }
    }
}