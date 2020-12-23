namespace YamhilliaNET.Constants
{
    public enum MemberType
    {
        /**
         * Can do everything ADMINISTRATOR can, plus delete and transfer farm.
         */
        OWNER = 3,
        
        /**
         * Can do everything a WORKER can, plus add new WORKERS, delete animals, and mark animals for sale
         */
        ADMINISTRATOR = 2,
        
        /**
         * Can create/edit animals, edit/view schedule, etc.
         */
        WORKER = 1,
        
        /**
         * Readonly view of animals, like for a potential client
         */
        GUEST = 0,
    }
}