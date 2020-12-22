namespace YamhilliaNET.Constants
{
    public enum MemberType
    {
        /**
         * Can do everything ADMINISTRATOR can, plus delete and transfer farm.
         */
        OWNER,
        
        /**
         * Can do everything a WORKER can, plus add new WORKERS, delete animals, and mark animals for sale
         */
        ADMINISTRATOR,
        
        /**
         * Can create/edit animals, edit/view schedule, etc.
         */
        WORKER,
        
        /**
         * Readonly view of animals, like for a potential client
         */
        GUEST
    }
}