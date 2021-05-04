package com.example.filetest;


import android.Manifest;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

public class ShowInfoFragment extends Fragment {

    String TAG = "debug";

    public ShowInfoFragment() {
        // Required empty public constructor
    }
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_show_info, container, false);

        requestPermissions( new String [] {Manifest.permission.READ_EXTERNAL_STORAGE}, 1);




        return view;
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        switch (requestCode) {
            case 1:
                // If request is cancelled, the result arrays are empty.
                if (grantResults.length > 0 &&
                        grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    try {
                        FileInputStream Fin= new FileInputStream(new File(Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DOWNLOADS), "text.txt"));
                        byte[] b=new byte[100];
                        Fin.read(b);
                        String text=new String(b);
                        Log.d(TAG, "Text: " + text);
                        Fin.close();
                    } catch (FileNotFoundException e) {
                        Log.d(TAG, "onCreateView: " + e.getMessage());
                    } catch (IOException e) {
                        Log.d(TAG, "onCreateView: " + e.getMessage());
                    }
                }  else {
                    Log.d(TAG, "onRequestPermissionsResult: Error ");
                }
                return;
        }
    }
}
/*
public class ShowInfoFragment extends Fragment {
    String TAG = "debug";
    //android.permission.WRITE_EXTERNAL_STORAGE //might need to add to manifest when writing to SDCard

    int PERMISSION_ALL = 1;
    String[] PERMISSIONS = {android.Manifest.permission.READ_EXTERNAL_STORAGE, android.Manifest.permission.WRITE_EXTERNAL_STORAGE};

    public ShowInfoFragment() {
        // Required empty public constructor
    }
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    TextView textViewTextData;
    @RequiresApi(api = Build.VERSION_CODES.R)
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_show_info, container, false);

        boolean hasPerm = hasPermissions(getContext(), PERMISSIONS);

        textViewTextData = view.findViewById(R.id.textViewTextData);

        StringBuilder text = new StringBuilder();

        try{
            //File dow = Environment.getRootDirectory(); //getStorageDirectory();
            File path = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DOWNLOADS); // Environment.getExternalStoragePublicDirectory(String.valueOf(Environment.getRootDirectory()));
            Log.d(TAG, "Downloads Path: " + path.list());

            File downloads = Environment.getDownloadCacheDirectory();
            Log.d(TAG, "onCreateView: " + downloads.list());
            File textFile = new File(downloads, "text.txt");


            BufferedReader br = new BufferedReader(new FileReader((textFile)));
            String line;

            while ((line = br.readLine()) != null) {
                text.append(line + "\n");

            }
            textViewTextData.setText(text);
        }catch(Exception e)  {
            Log.d(TAG, "ERROR OnCReate: " + e.getMessage());
            Toast.makeText(getContext(), "Error " + e.getMessage(), Toast.LENGTH_SHORT).show();
        }






        return view;
    }
    public static boolean hasPermissions(Context context, String... permissions) {
        if (android.os.Build.VERSION.SDK_INT >= Build.VERSION_CODES.M && context != null && permissions != null) {
            for (String permission : permissions) {
                if (ActivityCompat.checkSelfPermission(context, permission) != PackageManager.PERMISSION_GRANTED) {
                    return false;
                }
            }
        }
        return true;
    }
}


 */