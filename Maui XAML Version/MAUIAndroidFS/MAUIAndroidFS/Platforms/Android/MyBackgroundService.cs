using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using ClasesGenerales;
using Microsoft.AspNetCore.SignalR.Client;

namespace MAUIAndroidFS.Platforms.Android;

[Service]
internal class MyBackgroundService : Service
{
    Timer timer = null;
    int myId = (new object()).GetHashCode();
    int BadgeNumber = 0;
    NotificationCompat.Builder notification;
    HubConnection hubConnection;

    UdpCommunication PuertoEscucha;
    public override IBinder OnBind(Intent intent)
    {
        return null;
    }

    public override StartCommandResult OnStartCommand(Intent intent,
        StartCommandFlags flags, int startId)
    {
        var input = intent.GetStringExtra("inputExtra");

        var notificationIntent = new Intent(this, typeof(MainActivity));
        var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent,
            PendingIntentFlags.Immutable);

        notification = new NotificationCompat.Builder(this,
                MainApplication.ChannelId)
            .SetContentText(input)
            .SetSmallIcon(Resource.Drawable.AppIcon)
            .SetContentIntent(pendingIntent);

        // Increment the BadgeNumber
        BadgeNumber++;
        // set the number
        notification.SetNumber(BadgeNumber);
        // set the title (text) to Service Running
        notification.SetContentTitle("Service Running");
        // build and notify
        StartForeground(myId, notification.Build());

        // timer to ensure hub connection
        //timer = new Timer(Timer_Elapsed, notification, 0, 10000);
        timer = new Timer(Timer_Elapsed, notification, 0, 5000);


        // Mio
        PuertoEscucha = new UdpCommunication(9396);
        PuertoEscucha.RecibidosDatos += RecibidaComunicacion;
        // Fin mio

        // You can stop the service from inside the service by calling StopSelf();

        return StartCommandResult.Sticky;
    }

    // Mio
    int contador = 0;
    protected void RecibidaComunicacion(string ip, string datos)
    {
        MiUdp.Envia("RecibidaComunicacion()");
        try
        {
            //MainThread.BeginInvokeOnMainThread(() =>
            //{
                try
                {
                    MiUdp.Envia($"HasWindowFocus: {Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.HasWindowFocus}");
                //var notificationIntent = new Intent(this, typeof(MainActivity));
                //Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.StartActivityForResult(notificationIntent, 1);

                Intent main2 = new Intent(ApplicationContext, typeof(MainActivity));
                //Intent main2 = new Intent(this, typeof(MainActivity));
                //main2.SetFlags(ActivityFlags.SingleTop );
                main2.SetFlags( ActivityFlags.NewTask);
                    StartActivity(main2);

                    /*
Java.Lang.IllegalStateException
  Message=View with id -1: crc640ec207abc449b2ca.ShellPageContainer#onMeasure() did not set the measured dimension by calling setMeasuredDimension()                     */
                }
                catch (Exception ex)
                {
                    MiUdp.Envia(ex.ToString());
                }
            //});
        }
        catch (Exception ex)
        {
            MiUdp.Envia(ex.ToString());
        }
    }
    // Fin mio


    async Task EnsureHubConnection()
    {
        // Así parece que se lanza una actividad
        //Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.StartActivityForResult(notificationIntent, 1);

        // Lo comento para pruebas
        //if (hubConnection == null)
        //{
        //    hubConnection = new HubConnectionBuilder()
        //        .WithUrl("https://[YOUR-AZURE-SERVER-NAME].azurewebsites.net/BroadcastHub")
        //        .Build();

        //    hubConnection.On<string>("ReceiveMessage", (message) =>
        //    {
        //        // Display the message in a notification
        //        BadgeNumber++;
        //        notification.SetNumber(BadgeNumber);
        //        notification.SetContentTitle(message);
        //        StartForeground(myId, notification.Build());
        //    });
        //    try
        //    {
        //        await hubConnection.StartAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        // Put a breakpoint on the next line to debug
        //    }

        //}
        //else if (hubConnection.State != HubConnectionState.Connected)
        //{
        //    try
        //    {
        //        await hubConnection.StartAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        // Put a breakpoint on the next line to debug
        //    }
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    async void Timer_Elapsed(object state)
    {
        AndroidServiceManager.IsRunning = true;

        await EnsureHubConnection();

        // Mio
        contador++;
        if (contador == 5)
        {
            RecibidaComunicacion("", "");
        }

        MiUdp.Envia($"Servicio funcionando: {DateTime.Now.ToString("HH:mm:ss")}");

        try
        {
            AndroidServiceManager.EjecutaAccion();
        }
        catch (Exception ex)
        {
            MiUdp.Envia($"Excepción Accion: {ex.Message}");
        }

        // Fin mio
    }


}