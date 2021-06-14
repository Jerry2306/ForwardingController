# ForwardingController
Tunnel forwarding controller, currently based on Ngrok. 
Kinda newer version of the NgrokManager.

Concept:
  Main function:
    Start Tunnel -> Update in Database
    
  Planned feature:
                                                             TRUE -> update DateTime in Database
    Thread checking every some minute if tunnel is still open
                                                             FALSE -> remove entry from database
