﻿using Tournament.Core.DTO.TournamentDTO;
using Tournament.Core.Entities;

namespace Tournament.API.Mappings
{
    public class Mappings
    {
        //-----------EXTRACT INTERFACE-----------
        public static TournamentDTO TournamentToTournamentDTO(Tournament.Core.Entities.Tournament tournament)
        {
            //if (tournament == null)return BadRequest();
            return new TournamentDTO()
            {
                Title = tournament.Title,
                StartDate = tournament.StartDate
            };

        }

        public static Tournament.Core.Entities.Tournament TournamentDTOToTournament(TournamentDTO tournamentDTO)
        {
            //if (tournament == null)return BadRequest();
            return new Tournament.Core.Entities.Tournament()
            {
                Title = tournamentDTO.Title,
                StartDate = tournamentDTO.StartDate
            };

        }

        public static TournamentDTOUpdate TournamentToTournamentDTOUpdate(Tournament.Core.Entities.Tournament tournament)
        {
            //if (tournament == null)return BadRequest();
            return new TournamentDTOUpdate()
            {
                Title = tournament.Title,
                StartDate = tournament.StartDate,
                Id = tournament.Id
            };

        }

        public static Tournament.Core.Entities.Tournament TournamentDTOUpdateToTornament(Tournament.Core.Entities.Tournament tournament, TournamentDTOUpdate tournamentDTOUpdate)
        {
            //if (tournament == null)return BadRequest();
            tournament.Title = tournamentDTOUpdate.Title;
            tournament.StartDate = tournamentDTOUpdate.StartDate;

            return tournament;

        }
    }
}