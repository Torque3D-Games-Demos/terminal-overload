// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

// List of master servers to query, each one is tried in order
// until one responds
$pref::Master[0] = "1:master1.terminal-overload.org:28010";
$pref::Master[1] = "1:master2.terminal-overload.org:28010";
$pref::Master[2] = "1:master3.terminal-overload.org:28010";
// The following server will only be tried if all the above fail.
$pref::Master[3] = "2:master.garagegames.com:28002";

// List of AIMS pubservers
$Pref::AIMS::Server[0] = "aims.ddns.net";

// Information about the server
$Pref::Server::RegionMask = 1;
$Pref::Server::Name = "Terminal Overload Server";
$Pref::Server::Info = "This is a Terminal Overload server.";

// The connection error message is transmitted to the client immediatly
// on connection, if any further error occures during the connection
// process, such as network traffic mismatch, or missing files, this error
// message is display. This message should be replaced with information
// usefull to the client, such as the url or ftp address of where the
// latest version of the game can be obtained.
$Pref::Server::ConnectionError =
   "You do not have the correct version of Terminal Overload or "@
   "the related art needed to play on this server, please contact "@
   "the server operator for more information.";

// The network port is also defined by the client, this value 
// overrides pref::net::port for dedicated servers
$Pref::Server::Port = 28000;

// If the password is set, clients must provide it in order
// to connect to the server
$Pref::Server::Password = "";

// Password for admin clients
$Pref::Server::AdminPassword = "";

// Misc server settings.
$Pref::Server::MaxPlayers = 64;
$Pref::Server::TimeLimit = 20;               // In minutes
$Pref::Server::KickBanTime = 300;            // specified in seconds
$Pref::Server::BanTime = 1800;               // specified in seconds
$Pref::Server::FloodProtectionEnabled = 1;
$Pref::Server::MaxChatLen = 120;

// Default networking settings
$pref::Net::RegionMask = 1;
$pref::Net::PacketRateToServer = 32;
$pref::Net::PacketRateToClient = 32;
$pref::Net::PacketSize = 450;
