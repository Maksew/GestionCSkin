using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GestionCSkin.Model
{
    public class SkinService
    {
        private const string FilePath = "skins.json";
        
        public List<Skin> LoadSkins()
        {
            if (!File.Exists(FilePath)) return new List<Skin>();
            
            try 
            {
                var json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Skin>>(json);
            }
            catch 
            {
                return new List<Skin>();
            }
        }

        public void SaveSkins(List<Skin> skins)
        {
            var json = JsonConvert.SerializeObject(skins, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}