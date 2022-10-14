package com.moviles.logger;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.util.Log;

public class logger {
    private static logger instance = new logger();

    private static final String TAG = "L2022 => ";

    public static Activity mainActivity;

    public interface AlertViewCallback {
        public void OnButtonTapped(int id);
    }

    public static logger GetInstance() {
        Log.d("Get", "GetInstance");
        return instance;
    }

    public void MyLog(String str) {
        Log.d(TAG, str);
    }

    public void ShowAlertView(String[] strings, final AlertViewCallback callback) {
        if (strings.length < 3) {
            Log.i(TAG, "Error - Expected at least 3 string, got " + strings.length);
            return;
        }
        DialogInterface.OnClickListener myClickListener = new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
                Log.i(TAG, "Tapped: " + which);
            }
        };

        AlertDialog alertDialog = new AlertDialog.Builder(mainActivity)
                .setTitle(strings[0])
                .setMessage(strings[1])
                .setCancelable(false)
                .create();
        alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL,strings[2],myClickListener);
        if(strings.length>3)
            alertDialog.setButton(AlertDialog.BUTTON_NEGATIVE,strings[3],myClickListener);
        if(strings.length>4)
            alertDialog.setButton(AlertDialog.BUTTON_POSITIVE,strings[4],myClickListener);
        alertDialog.show();
    }
}