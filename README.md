# ElminageGothic

# Using the current patch:
  Start the game. Hammer X until the cutscene begins, and then close the game. You now have an MS0:\PSP\SAVEDATA\ULJM06091_02_INST folder on your PSP/PSP-like device.  
  Apply etc.ips to etc.bin in that folder.  

# Tool requirements for the creation of a patch:
  BraUnpacker. I am unsure who made this tool, or where to find it at the moment.  
  Elminage Gothic for PC.  
  Elminage Gothic: Ulm Zakir to Yami no Gishiki for PSP.  
  QuickBMS, and the elminage QuickBMS script you can find on Luigi Auriemma's page at http://aluigi.altervista.org/.  
  PGD Encrypter by Belmondo at http://wololo.net/talk/viewtopic.php?f=5&t=46006.  

# How to create a patch:
  Use BraUnpacker on the English version of Elminage Gothic. This will give you three folders: s00, s01, and s02. s00 is the one you want.  
  Use PGD Encrypter to decrypt the etc.bin in /INSDIR on the Elminage Gothic PSP UMD. Feed the resulting Data.CPK (a hardcoded name) into QuickBMS.   
  You now have two things: An s00 folder containing the game's scripts in csv files, and a PSP folder containing the game's scripts in japanese csv files. It is not as simple as overwriting contents, however.  
  Once you're done editing things, you have to use quickbms to reimport everything into data.cpk. Use a command like "quickbms -r -w elminage.bms ETC.BIN.decrypt "C:\games\emulation\ppsspp\work insdir\decrypted"" to do so, and then rename the resulting ETC.BIN.decrypt data.cpk and run PGD Encrypter to finalize the project. Copy the result of all of this to MS0:\PSP\SAVEDATA\ULJM06091_02_INST and you're set.  

# Working with CSV files:
Use Excel set to Shift-JIS. If you see large notes, it is a good idea to dummy them out with a question mark or other single character. The reason for this is that when you use quickbms to reimport, it will complain if files are bigger than they were in the original archive.   

# Notes on specific files:
trap.csv: This can be copied from the English version. Unfortunately, the game has hardcoded the trap names for half the menu, so it is of questionable use.  
monster1.csv/monster2.csv/monster3.csv: These can be copied directly.  
place_nm.csv: This cannot be directly imported. You have to use Excel or another csv program to copy the names manually. They also run into a problem we'll be addressing later.  
dgn_set.csv: The names in the travel menu can be too wide for the menu. Caves of Tsun-Kurn is the first dungeon, which is already stretching the UI past its limit. It would be best to carefully trim these names.  
item_help.csv: Do not copy the PC version directly. Copy the english text, don't touch anything else. You will likely have to trim a lot of item names to get this to work.   
spell.csv: Do not touch this file. Touching this file leads to the complete removal of magic in the world. I have a vague suspicion that it has to do with player.csv, but I have not managed to figure that out yet.  
spell_text.csv: Works wonderfully. Improves the playability of the game massively once you port the text over. Frankly, I'd rather have diacritics I don't understand with a working description than the original names without them. Warning: This requires you to force an import with quickbms, but I haven't seen any issues.  
help_text.csv: There is specifically a column for the PSP! It's wrong, though. Importing the English text directly over it will definitely result in misalignment of a lot. It still helps the game's playability somewhat.  
player.csv: Just copy the names. There is a lot of power in this file.  
event_text.csv: This is a hell of a file. All the quest text is in triplicate. It can be imported, but without functional line breaks you will get massive offscreen text.  

# Where do we go from here?
A massive amount of the game's strings are stored directly in the eboot. If you want to get at those, run PPSSPP and activate DevTools. Tell it to decrypt the eboot for Elminage Gothic and you can find them in there.   

You'll be limited quite a lot by this. For example, 守られている is used in both EO and EG and actually shows up ingame in the official Ghostlight translation of Elminage Original. As far as I understand, it means "It is protected!". Modifying the binary directly with a hex editor will give you the ability to replace this with Japanese Character Length * 2, so you could fit 12 characters in there theoretically.   

Additionally, a staggering amount of the game is stored in textures that are not compatible with the format that the game is loading them as. PPSSPP can dump these, but reinserting them would require a modded PSP using a very complex FreeCheat plugin or the reverse engineering of what's wrong with the textures. An example of one of them:   
![orig texture](http://imgur.com/4Knozx8.png)

There's also a big issue I haven't addressed yet. Monster names have imported correctly, as have place names. Unfortunately, the top right of the screen's print function only supports up to around 8 characters. This will have to be addressed to continue with the project. There are two ways to do so: We either have to hijack the function that prints in the eboot and tell it to go further left (as it is actually printing offscreen), or we have to trim every monster and place name down to 8 characters. The latter is crude, so I am currently researching methods of doing the first method gracefully.   

# Before, After, and Official:

The shop is the best example of almost everything going right.  

Before:    
![before](http://i.imgur.com/fJjiEet.jpg)
Patch:  
![patch](http://i.imgur.com/9ksrJH6.jpg)
Official:   
![official](http://images.akamai.steamusercontent.com/ugc/93847375950866889/5D4655924F3C3E326C0B0968313430490F68F214/)
