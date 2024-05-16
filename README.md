# ADHD-Project-Server
 
This project helps refocus users who get distracted while completing their work. It does this by tracking the user's attentiveness using a combination of computer usage (is the user interacting with the KB + Mouse), gaze (where the user is looking), and position (has the user left the workspace). If the user is not paying attention according to any of these metrics, a sound will be played from the Hololens 2 telling them to focus. 


# How to use it

This is the server software to run the project. To use this software, you must first install and launch the server side (this one) and then launch the client version of the software on a Micosoft Hololens 2. Enter the IP of the server computer into the hololens and it will connect and the user may begin to complete any work on the PC. 
* If you need to get the IP of the server computer. Launch a command prompt on the server, type ipconfig and copy down the IPv4 address. This will be what is entered on the client side in virtual reality.



# Troubleshooting

There may be an issue with the client failing to connect to the server when the correct server IP is entered. In this case, it may be due to Windows Firewall rules preventing the TCP connection between the client and server. You can create a rule to allow the connection on port 32401.


# Plugins used:

https://github.com/elringus/unity-raw-input used for determining when the user was interacting with their keyboard
