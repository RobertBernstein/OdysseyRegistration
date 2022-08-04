function PrintThisPage()
{
	// Anywhere you want to use this function to print out part of a page,
	// just surround the part to be printed with <div id=contentstart>...</div> 
	var sOption="toolbar=yes,location=no,directories=yes,menubar=yes,"; 
		sOption+="scrollbars=yes,width=750,height=600,left=100,top=25";

	var sWinHTML = document.getElementById("printableContent").innerHTML; 

	var winprint=window.open("","",sOption); 

	winprint.document.open(); 
	winprint.document.write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
	winprint.document.write("<html>\n<body>\n"); 
	winprint.document.write("<title>" + document.title + "</title>\n");
	winprint.document.write("</head>\n<body>\n");
	winprint.document.write(sWinHTML);
	winprint.document.write("<p align=\"center\"><input type=\"button\" value=\"Print This Page\" onClick=\"print(document)\"></p>\n");
	winprint.document.write("</body>\n</html>\n"); 
	winprint.document.close();
	winprint.focus();
}
