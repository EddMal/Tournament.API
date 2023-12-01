using Tournament.Core.DTO.TournamentDTO;

namespace Tournament.API.Mappings
{
    public interface IMappings
    {
        public TournamentDTO TournamentToTournamentDTO(Tournament.Core.Entities.Tournament tournament);

        public Tournament.Core.Entities.Tournament TournamentDTOToTournament(TournamentDTO tournamentDTO);

        public TournamentDTOUpdate TournamentToTournamentDTOUpdate(Tournament.Core.Entities.Tournament tournament);

        public Tournament.Core.Entities.Tournament TournamentDTOUpdateToTornament(Tournament.Core.Entities.Tournament tournament, TournamentDTOUpdate tournamentDTOUpdate);

        public IEnumerable<TournamentDTO> TournamentsToTournamentDTOs(IEnumerable<Tournament.Core.Entities.Tournament> tournaments);


    }
}