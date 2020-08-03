/* 
    AEB Adobe Acrobat Reader JavaScript Methods

    Copyright (C) 2019 AcroTeX.Net
    D. P. Story
    http://www.acrotex.net
    
    Version 1.0
*/
// console.println("Version 1.0 (Reader DC)");
if ( typeof aebTrustedFunctions == "undefined") {
    aebTrustedFunctions = app.trustedFunction( function ( doc, oFunction, oArgs )
    {
        app.beginPriv();
            var retn = oFunction( oArgs, doc );
        app.endPriv();
        return retn;
    });
}
aebSaveAs = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        app.execMenuItem("Save");
    app.endPriv();
});
aebDocSaveAs = app.trustPropagatorFunction( function ( oArgs, doc )
{
    var Msg=function(e){return (aebDocSaveAs.msg==undefined)?("Doc SaveAs Error: " + e.toString()):aebDocSaveAs.msg;}
    var Action=function(){return ((aebDocSaveAs.action==undefined)?null:eval(aebDocSaveAs.action));}
    app.beginPriv();
    try { 
        return retn = doc.saveAs(oArgs); 
    } catch(e){console.println(Msg(e));Action();}
    app.endPriv();
    aebDocSaveAs.msg=undefined;
    aebDocSaveAs.action=undefined;
});
aebBrowseForDoc = app.trustPropagatorFunction( function ( oArgs )
{
    app.beginPriv();
        return retn = app.browseForDoc(oArgs);
    app.endPriv();
});
aebLaunchURL=app.trustPropagatorFunction ( function ( oArgs )
{
    app.beginPriv();
      var retn = app.launchURL(oArgs);
    app.endPriv();
});

