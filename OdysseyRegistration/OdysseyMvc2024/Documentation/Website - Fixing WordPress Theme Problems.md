# Website - Fixing WordPress Theme Problems
If the WordPress theme used by the web site is broken, you can delete or move the directory in which the theme is currently installed so that you may reinstall a fresh copy. To do so, you need to install an FTP client on your computer. This includes programs such as CuteFTP, FileZilla, and FTP Explorer. You may wish to see the link below for configuring these programs.

<https://support.winhost.com/KB/c241/ftp-program-configuration.aspx>

For our site, you need to use the following FTP settings:

| Setting | Value |
|---|---|
| FTP Site | [ftp.novanorth.org](ftp://ftp.novanorth.org) |
| FTP Port (FTPS Protocol, Explicit SSL) | 21 |
| Username | vaodysse |
| Full URL (use instead of above information) | ftps://vaodysse@ftp.novanorth.org/ |

You should also navigate to the WordPress Themes page by logging in as the administrator and connecting to the Themes page. You can do this by logging in at:

> <https://www.novanorth.org/wp/wp-admin/>

and navigating to Appearance / Themes or by connecting directly to this page:

> <https://www.novanorth.org/wp/wp-admin/themes.php>

To rename the folder containing the current theme, e.g. "Graphene", return to your FTP client and navigate to the following folder:

> /wp/wp-content/themes/graphene

You may also reference this folder using its full path in some FTP clients:

> ftps://vaodysse@ftp.novanorth.org/wp/wp-content/themes/grapheme

I was going to just rename the folder from "graphene" to "graphene.old" so that WordPress would no longer find it. My hope was that by doing so, WordPress would not recognize the theme and would let me reinstall "Graphene". However, refreshing the Themes web page still showed the theme as installed and located in the "graphene.old" folder. So, I moved it up a folder level to the /wp/wp-content directory. Now you can return to the WordPress Themes web page, refresh the page to ensure that "Graphene" is no longer installed. Then click on the "Install Themes" tab at the top of the page. Enter "Graphene" in the Search textbox and click the "Install Now" link. Once the theme has been installed, click the Activate link.

The theme should now be installed correctly. You may use your FTP client to delete the old, moved "graphene\" directory.
