package com.moviles.logger;

import android.util.Log;

public class logger {
    private static  logger instance = new logger();

    private static final String TAG= "L2022 => ";

    public static logger GetInstance(){
        Log.d("asd","GetInstance");
        return instance;
    }

    public void MyLog(String str)
    {
        Log.d(TAG,str);
    }
}