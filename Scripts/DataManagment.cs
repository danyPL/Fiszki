using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fiszki.Scripts
{
    abstract class DataManagment : Paths, IDataManagement
    {
        public List<User>? flash_cards;
      

        public virtual void LoadData()
        {
            string flashcards_J = File.ReadAllText(PathFlashCards);
           
            flash_cards = JsonSerializer.Deserialize<List<User>>(flashcards_J);
           
        }

        public virtual void PushData(object data, ActionTypes type)
        {
            switch (type)
            {
                case ActionTypes.User:
                    users = (List<User>)data;
                    string jsonU = JsonSerializer.Serialize(users);
                    File.WriteAllText(PathUsers, jsonU);
                    break;
                case ActionTypes.Room:
                    rooms = (List<Room>)data;
                    string jsonR = JsonSerializer.Serialize(rooms);
                    File.WriteAllText(PathRooms, jsonR);
                    break;
                case ActionTypes.Movie:
                    movies = (List<Movie>)data;
                    string jsonM = JsonSerializer.Serialize(movies);
                    File.WriteAllText(PathMovies, jsonM);
                    break;
                case ActionTypes.Cinema:
                    cinemas = (List<Cinema>)data;
                    string jsonC = JsonSerializer.Serialize(cinemas);
                    File.WriteAllText(PathCinemas, jsonC);
                    break;
                default:
                    throw new ArgumentException("Invalid data type.");
            }
        }
    }
