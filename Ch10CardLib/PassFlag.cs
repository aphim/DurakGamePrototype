using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    /// <summary>
    /// Class used to store flag data. PassFlag when tripped, will bypass the remain rounds and jump straight to the end of round.
    /// AttackerWin and DefenderWin flags are tripped with the corresponding player wins the round
    /// </summary>
    public class PassFlag
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PassFlag()
        {
        }

        /// <summary>
        /// getters and setters for the passflag
        /// </summary>
        public bool passFlag { get; set; }

        /// <summary>
        /// getters and setters for the attackerwin flag
        /// </summary>
        public bool attackerWin { get; set;}

        /// <summary>
        /// getters and setters for the defenderwin flag
        /// </summary>
        public bool defenderWin { get; set;}


    }
}
