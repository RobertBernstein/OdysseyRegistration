function PrintThisPage()
{
	// Anywhere you want to use this function to print out part of a page,
	// just include a <div id="printableContent"> with the content you want to print.
	var printableContent = document.getElementById("printableContent");
	if (!printableContent) {
		console.error("PrintThisPage: #printableContent was not found.");
		return;
	}

	var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,";
	sOption += "scrollbars=yes,width=750,height=600,left=100,top=25";

	var sWinHTML = printableContent.innerHTML;
	var printMarkup = "";
	printMarkup += "<!DOCTYPE html>\n";
	printMarkup += "<html>\n";
	printMarkup += "<head>\n";
	printMarkup += "<meta charset=\"utf-8\">\n";
	printMarkup += "<title>" + document.title + "</title>\n";
	printMarkup += "<style>body{font-family:Segoe UI,Verdana,Helvetica,sans-serif;font-size:14px;margin:16px;} .print-actions{text-align:center;margin-top:16px;}</style>\n";
	printMarkup += "<script>function popupPrint(){var printed=false;try{window.focus();if(typeof window.print==='function'){window.print();printed=true;}}catch(e){console.error('popupPrint window.print failed',e);}if(!printed){try{printed=document.execCommand&&document.execCommand('print',false,null);}catch(e2){console.error('popupPrint execCommand failed',e2);}}if(!printed&&window.opener&&!window.opener.closed){try{window.opener.focus();if(typeof window.opener.print==='function'){window.opener.print();printed=true;}}catch(e3){console.error('popupPrint opener.print failed',e3);}}if(!printed){var m=document.getElementById('printHelp');if(m){m.style.display='block';}}return printed;}window.addEventListener('DOMContentLoaded',function(){var b=document.getElementById('popupPrintButton');if(b){b.addEventListener('click',popupPrint);}setTimeout(function(){popupPrint();},100);});<\/script>\n";
	printMarkup += "</head>\n";
	printMarkup += "<body>\n";
	printMarkup += sWinHTML;
	printMarkup += "<p class=\"print-actions\"><button type=\"button\" id=\"popupPrintButton\" onclick=\"popupPrint()\">Print This Page</button></p>\n";
	printMarkup += "<p id=\"printHelp\" class=\"print-actions\" style=\"display:none\">Printing may be blocked in this embedded browser. Use <strong>Ctrl+P</strong> (or <strong>Cmd+P</strong> on Mac).</p>\n";
	printMarkup += "</body>\n";
	printMarkup += "</html>\n";

	var blob = new Blob([printMarkup], { type: "text/html" });
	var printUrl = URL.createObjectURL(blob);
	var winprint = window.open(printUrl, "_blank", sOption);

	if (!winprint) {
		alert("Unable to open print window. Please allow pop-ups for this site.");
		return;
	}

	winprint.addEventListener("load", function () {
		try {
			winprint.focus();
		}
		catch (e) {
			console.error("PrintThisPage: popup focus failed.", e);
		}
	});

	// Revoke the blob URL after the popup has had time to load.
	setTimeout(function () {
		URL.revokeObjectURL(printUrl);
	}, 30000);
}
