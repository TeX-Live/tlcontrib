/* 
    AEB Pro Document Assembly Methods

    Copyright (C) 2012 -- 2019 AcroTeX.Net
    D. P. Story
    http://www.acrotex.net
    
    Version 1.7.2
*/
// console.println("Version 1.7.2");
if ( typeof aebTrustedFunctions == "undefined") {
    aebTrustedFunctions = app.trustedFunction( function ( doc, oFunction, oArgs )
    {
        app.beginPriv();
            var retn = oFunction( oArgs, doc );
        app.endPriv();
        return retn;
    });
}
aebAddWatermarkFromFile = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return retn = doc.addWatermarkFromFile(oArgs);
    app.endPriv();
});
aebImportIcon = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return retn = doc.importIcon(oArgs);
    app.endPriv();
});
aebInsertPages = app.trustPropagatorFunction( function ( oArgs, doc )
{
    var Msg=function(e){return (aebInsertPages.msg==undefined)?("Insert Pages Error: " + e.toString()):aebInsertPages.msg;}
    app.beginPriv();
        try { doc.insertPages(oArgs); } catch(e) {console.println(Msg(e));}
    app.endPriv();
    aebInsertPages.msg=undefined;
})
aebAppOpenDoc = app.trustPropagatorFunction( function ( oArgs, doc )
{
    var Msg=function(e){return (aebAppOpenDoc.msg==undefined)?("App Open Doc Error: " + e.toString()):aebAppOpenDoc.msg;}
    app.beginPriv();
        try { var retn = app.openDoc(oArgs); } catch(e) {console.println(Msg(e));}
    app.endPriv();
    aebAppOpenDoc.msg=undefined;
    return retn;
})
aebImportTextData = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return retn = doc.importTextData(oArgs);
    app.endPriv();
});
aebImportSound = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return retn = doc.importSound(oArgs);
    app.endPriv();
});
aebSaveAs = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        app.execMenuItem("Save");
    app.endPriv();
});
// Version 1.7.2
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
aebExtractPages = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return retn = doc.extractPages(oArgs);
    app.endPriv();
});
aebMailDoc = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return retn = doc.mailDoc(oArgs);
    app.endPriv();
});
aebImportDataObject = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return retn = doc.importDataObject(oArgs);
    app.endPriv();
});
aebSignatureSign = app.trustPropagatorFunction( function ( oArgs, field )
{
    app.beginPriv();
        return retn = field.signatureSign(oArgs);
    app.endPriv();
});
aebSecurityHandlerLogin = app.trustPropagatorFunction( function ( oArgs, securityHandler )
{
    app.beginPriv();
        return retn = securityHandler.login(oArgs);
    app.endPriv();
});
aebSecurityGetHandler = app.trustPropagatorFunction( function ( oArgs, security )
{
    app.beginPriv();
        return retn = security.getHandler(oArgs);
    app.endPriv();
});
aebAppGetPath = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        var retn = app.getPath(oArgs);
    app.endPriv();
    return retn;
});
aebSignatureSetSeedValue = app.trustPropagatorFunction( function ( oArgs, field )
{
    app.beginPriv();
        return retn = field.signatureSetSeedValue(oArgs);
    app.endPriv();
});
aebAddIcon=app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        doc.addIcon(oArgs);
    app.endPriv();
});
aebCreateTemplate = app.trustPropagatorFunction( function ( oArgs, doc )
{
    var Msg=function(e){return (aebCreateTemplate.msg==undefined)?("Create Template Error: " + e.toString()):aebCreateTemplate.msg;}
    app.beginPriv();
        try { doc.createTemplate(oArgs); } catch(e) {console.println(Msg(e));}
    app.endPriv();
    aebCreateTemplate.msg=undefined;

})
// Version 1.7.1 removed doc from arg in browse and launch
aebBrowseForDoc=app.trustPropagatorFunction( function ( oArgs )
{
    app.beginPriv();
      var retn = app.browseForDoc(oArgs);
    app.endPriv();
});
// added aebLaunchURL Version 1.7
aebLaunchURL=app.trustPropagatorFunction ( function ( oArgs )
{
    app.beginPriv();
      var retn = app.launchURL(oArgs);
    app.endPriv();
});
