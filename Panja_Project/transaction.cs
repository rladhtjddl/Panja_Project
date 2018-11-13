using System;

public class Transaction
{
    
    private Transaction()
    {
        //Prepare connection according to method of transaction 
    }


    private static class singleInstance
    {
        public static const Transaction INSTANCE = new Transaction();
    }

    public static Transaction getInstance ()
    {
        return singleInstance.INSTANCE;
    }
    
    //Send Method

    //Receive Method
}
