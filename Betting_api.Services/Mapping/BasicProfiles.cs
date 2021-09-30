using AutoMapper;
using Betting_api.DB_Models;
using evona_hackathon.Models.V1_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Services.Mapping
{
    public class BasicProfile:Profile
    {
        public BasicProfile()
        {
            CreateMap<Korisnik,User>();
            CreateMap<UserUpsertRequest, Korisnik>();
            CreateMap<Igra, Game>();
            CreateMap<GameUpsertRequest, Igra>();
        }
    }
}
