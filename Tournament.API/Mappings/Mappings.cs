using System.Collections.Generic;
using Tournament.Core.DTO.GameDTO;
using Tournament.Core.DTO.TournamentDTO;
using Tournament.Core.Entities;
using static Tournament.Core.DTO.SeedDTO.SeedDTO;

namespace Tournament.API.Mappings
{
    public class Mappings:IMappings
    {
        //-----------EXTRACT INTERFACE-----------
        public  TournamentDTO TournamentToTournamentDTO(Tournament.Core.Entities.Tournament tournament)
        {
            //if (tournament == null)return BadRequest();
            
                List < GameDTO > gamesDTOs = new List<GameDTO>();

                //foreach (var game in tournament.Games)
                //{
                //    gamesDTOs.Add(GameToGameDTO(game));
                //}
                
                return new TournamentDTO()
                {
                Title = tournament.Title,
                StartDate = tournament.StartDate,
                //Games = gamesDTOs
                };

        }

        public  Tournament.Core.Entities.Tournament TournamentDTOToTournament(TournamentDTO tournamentDTO)
        {
            //if (tournament == null)return BadRequest();
            return new Tournament.Core.Entities.Tournament()
            {
                Title = tournamentDTO.Title,
                StartDate = tournamentDTO.StartDate
            };

        }

        public  TournamentDTOUpdate TournamentToTournamentDTOUpdate(Tournament.Core.Entities.Tournament tournament)
        {
            //if (tournament == null)return BadRequest();
            return new TournamentDTOUpdate()
            {
                Title = tournament.Title,
                StartDate = tournament.StartDate,
                Id = tournament.Id
            };

        }

        public  Tournament.Core.Entities.Tournament TournamentDTOUpdateToTornament(Tournament.Core.Entities.Tournament tournament, TournamentDTOUpdate tournamentDTOUpdate)
        {
            //if (tournament == null)return BadRequest();
            tournament.Title = tournamentDTOUpdate.Title;
            tournament.StartDate = tournamentDTOUpdate.StartDate;

            return tournament;

        }

        public IEnumerable<TournamentDTO> TournamentsToTournamentDTOs(IEnumerable<Tournament.Core.Entities.Tournament> tournaments)
        {
            //if (tournament == null)return BadRequest();

            List<TournamentDTO> tournamentDTOs = new List<TournamentDTO>();
            
            foreach (var tournament in tournaments)
            {
                List<GameDTO> gamesDTOs = new List<GameDTO>();
                foreach (var game in tournament.Games)
                {
                    gamesDTOs.Add(GameToGameDTO(game));
                }

                TournamentDTO tournamentDTO = new TournamentDTO()
                {
                    Title = tournament.Title,
                    StartDate = tournament.StartDate,
                    Games = gamesDTOs,
                };
               
                    tournamentDTOs.Add(tournamentDTO);
            }

            return (tournamentDTOs);

        }

        //GAMES

        public GameDTO GameToGameDTO(Tournament.Core.Entities.Game game)
        {
            //if (tournament == null)return BadRequest();
            return new GameDTO()
            {
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId
            };

        }

        public Tournament.Core.Entities.Game GameDTOToGame(GameDTO gameDTO)
        {
            //if (tournament == null)return BadRequest();
            return new Tournament.Core.Entities.Game()
            {
                Title = gameDTO.Title,
                TournamentId = gameDTO.TournamentId,
                Time = gameDTO.Time
            };

        }

        public GameDTOUpdate GameToGameDTOUpdate(Tournament.Core.Entities.Game game)
        {
            //if (tournament == null)return BadRequest();
            return new GameDTOUpdate()
            {
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId,
                Id = game.Id
            };

        }

        public Tournament.Core.Entities.Game GameDTOUpdateToGame(Tournament.Core.Entities.Game game, GameDTOUpdate gameDTOUpdate)
        {
            //if (tournament == null)return BadRequest();
            game.Title = gameDTOUpdate.Title;
            game.Time = gameDTOUpdate.Time;
            game.TournamentId = gameDTOUpdate.TournamentId;

            return game;

        }

        public IEnumerable<GameDTO> GamesToGameDTOs(IEnumerable<Tournament.Core.Entities.Game> games)
        {
            //if (tournament == null)return BadRequest();

            List<GameDTO> gameDTOs = new List<GameDTO>();

            foreach (var game in games)
            {
                GameDTO gameDTO = new GameDTO()
                {
                    Title = game.Title,
                    Time = game.Time,
                    TournamentId = game.TournamentId,
                };
                gameDTOs.Add(gameDTO);
            }

            return (gameDTOs);

        }

    }

}
