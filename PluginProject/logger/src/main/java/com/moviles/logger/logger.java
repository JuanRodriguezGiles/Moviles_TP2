package com.moviles.logger;

import android.app.Activity;
import android.app.AlertDialog;

import android.content.DialogInterface;
import android.util.Log;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;


public class logger {
    private static logger instance = new logger();

    public static logger GetInstance() {
        Log.d("Get", "GetInstance");
        return instance;
    }

    public static Activity mainActivity;

    public interface AlertViewCallback {
        public void OnButtonTapped(int id);
    }

    private static final String TAG = "L2022 => ";
    private static final String fileName = "logs.txt";
    //--------------------------------------------

    public void MyLog(String str) {
        Log.d(TAG, str);
        WriteFile(str);
    }

    public void ShowAlertView(String[] strings, final AlertViewCallback callback) {
        if (strings.length < 3) {
            Log.i(TAG, "Error - Expected at least 3 string, got " + strings.length);
            return;
        }
        DialogInterface.OnClickListener myClickListener = new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                callback.OnButtonTapped(which);
               //callback.notifyAll();
                dialog.dismiss();
                Log.i(TAG, "Tapped: " + which);
            }
        };

        AlertDialog alertDialog = new AlertDialog.Builder(mainActivity)
                .setTitle(strings[0])
                .setMessage(strings[1])
                .setCancelable(false)
                .create();
        alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, strings[2], myClickListener);
        if (strings.length > 3)
            alertDialog.setButton(AlertDialog.BUTTON_NEGATIVE, strings[3], myClickListener);
        if (strings.length > 4)
            alertDialog.setButton(AlertDialog.BUTTON_POSITIVE, strings[4], myClickListener);
        alertDialog.show();
    }

    public void WriteFile(String data) {
        data = data + "\n";
        File path = mainActivity.getFilesDir();
        File file = new File(path, fileName);

        try {
            if (file.exists()) {
                FileWriter file2 = new FileWriter(file, true);
                file2.write(data);
                file2.close();
            } else {
                FileOutputStream stream = new FileOutputStream(file);
                try {
                    stream.write(data.getBytes());
                } finally {
                    stream.close();
                }
            }
        } catch (IOException e) {
            Log.e("Exception", "File write failed: " + e.toString());
        }
    }

    public void ClearLogs()
    {
        File path = mainActivity.getFilesDir();
        File file = new File(path, fileName);

        if (!file.exists())
            return;

        mainActivity.deleteFile(fileName);
    }

    public String ReadFile() {
        File path = mainActivity.getFilesDir();

        File file = new File(path, fileName);
        if (!file.exists())
            return "";

        int length = (int) file.length();
        byte[] bytes = new byte[length];

        try {
            FileInputStream stream = new FileInputStream(file);
            try {
                stream.read(bytes);
            } finally {
                stream.close();
            }
        } catch (IOException e) {
            Log.e("Exception", "File write failed: " + e.toString());
        }

        return new String(bytes);
    }
}