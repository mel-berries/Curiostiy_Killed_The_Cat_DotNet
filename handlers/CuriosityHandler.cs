using System.Collections.Generic;
using Curiosity.Models;
using Curiosity.Services;
using RestSharp;

using Microsoft.Extensions.DependencyInjection;

namespace Curiosity.Handler{
    public class CuriosityHandler{
        private readonly IServiceProvider _serviceProvider;

        public CuriosityHandler(IServiceProvider serviceProvider){
            _serviceProvider = serviceProvider;
        }

        static void Main(string[] args){
            CuriosityService cInstance = new CuriosityService();

            var listOfImages = cInstance.GetImages("Curiosity");
            var sightings = cInstance.FindCat(listOfImages);
            cInstance.InsertInfo(listOfImages, "./catData.csv");
            var isFound = cInstance.InsertInfo(sightings, "./catSightings.csv");
            //if((sightings.Count){
               // cInstance.Email();
            //}
        }

    }
}
