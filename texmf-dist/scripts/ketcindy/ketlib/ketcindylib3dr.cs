//  Copyright (C)  2014  Setsuo Takato, KETCindy Japan project team
//
//This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
//

println("ketcindylib3d(2017.10.07) loaded");

//help:start();

Ketinit3d():=Ketinit3d(1);
Ketinit3d(sf):=(
  regional(tmp);
  tmp=round(4*SW.y)/4;
//  Ketinit3d(sf,[round(SW.x)+1,tmp-0.5,tmp-1]);
  Ketinit3d(sf,[-5,tmp-0.5,tmp-1]);
);
Ketinit3d(subflg,position):=(
//help:Ketinit3d(subflg=1)
  regional(ctr,tmp,tmp1,tmp2,tmp3,tmp4,xPos,yTh,yPh);
//  println("KETCindy3d V.2.3.5(2016.02.03)");
  BezierNumber3=1;   //15.02.28
  if(!islist(BZLIST3),
    BZLIST3=[]; //15.02.28
  ,
    tmp=apply(allpoints(),text(#));
	tmp1=select(tmp,substring(#,length(#)-1,length(#))=="p");
    tmp2=[];
    forall(tmp1,ctr,
      tmp=substring(ctr,0,length(ctr)-1);
      tmp=select(BZLIST3,#_1==tmp);
      if(length(tmp)>0,
        tmp2=append(tmp2,tmp_1);
      );
    );
    BZLIST3=tmp2;
  );
  SUBSCR=subflg;
  Setwindow("Msg=no");
  if(length(position)>2,
    xPos=position_1;
    yTh=position_2; 
    yPh=position_3;
  ,
    xPos=0;
    yTh=position_1; 
    yPh=position_2;
  );
//  THETA=60*pi/180;
//  PHI=30*pi/180;
  FocusPoint=[0,0,0];
  EyePoint=[5,5,5];
  Listplot("th",[[xPos,yTh],[xPos+9,yTh]],["notex","linecolor->[0,1,0]","Msg=no"]);
  Listplot("ph",[[xPos,yPh],[xPos+9,yPh]],["notex","linecolor->[0,1,0]","Msg=no"]);
     //16.02.10
  PutonCurve("TH",sgth,[xPos,xPos+9,"Msg=no"]);
  PutonCurve("FI",sgph,[xPos,xPos+9,"Msg=no"]);
  THETA=(TH.x-xPos)*20*pi/180;
  PHI=(FI.x-xPos)*40*pi/180;
//  Defvar("THETA",THETA); // 16.06.20
//  Defvar("PHI",PHI); // 16.06.20
  drawtext([xPos-0.8,yTh-0.1],Sprintf((TH.x-xPos)*20,2),align->"right"); //17.05.02
  drawtext([xPos-0.8,yPh-0.1],Sprintf((FI.x-xPos)*40,2),align->"right"); //17.05.02
  tmp="Setangle("+text((TH.x-xPos)*20)+","+text((FI.x-xPos)*40)+")";
  GLIST=append(GLIST,tmp);
  Addax(0);
  TSIZE=10;
  TSIZEZ=10;
  ErrFlg=0;
);

Start3d():=Start3d([]); //16.11.12
Start3d(ptexception):=( //16.11.12
//help:Start3d();
//help:Start3d(execptionptlist);
  regional(xmn,xMx,ymn,yMx,pt,pt3,pt2,
    xPos,yTh,yPh,Eps,tmp,tmp1,tmp2,tmp3,tmp4);
  GCLIST=[];
  GLIST=[];
  FUNLIST=[];
  COM0thlist=[];
  COM1stlist=[];
  COM2ndlist=[];
  SEG3dlist=[];
  OutFileList=[];
  FigPdfList=[];  // 16.04.08
  ErrFlag=0;
  OBJCMD=[]; // 16.11.29from
  OCNAME=Fhead;
  OCOPTION=["m","v"];// 16.11.29upto
  ArrowlineNumber=1;  // 16.01.31
  ArrowheadNumber=1;
  BezierNumber=1; //16.01.31
  BezierNumber3=1; //16.08.19
  Fnametex=Fhead+".tex";  // 15.04.06
  FnameR=Fhead+".r";
  FnameRbody=Fhead+"body.r";
  Fnameout=Fhead+".txt";
//  Ketinit3d(SUBSCR); // 16.06.20
  Setwindow("Msg=no"); // 16.06.20from
  tmp=round(4*SW.y)/4;
  xPos=-5;yTh=tmp-0.5;yPh=tmp-1;
  Listplot("th",[[xPos,yTh],[xPos+9,yTh]],
    ["notex","linecolor->[0,1,0]","Msg=no"]);
  Listplot("ph",[[xPos,yPh],[xPos+9,yPh]],
    ["notex","linecolor->[0,1,0]","Msg=no"]);
     //16.02.10
  PutonCurve("TH",sgth,[xPos,xPos+9,"Msg=no"]);
  PutonCurve("FI",sgph,[xPos,xPos+9,"Msg=no"]);
  drawtext([xPos-0.8,yTh-0.1],Sprintf((TH.x-xPos)*20,2),align->"right"); //17.05.02
  drawtext([xPos-0.8,yPh-0.1],Sprintf((FI.x-xPos)*40,2),align->"right"); //17.05.02
  tmp="Setangle("
    +format((TH.x-xPos)*20,5)+","
    +format((FI.x-xPos)*40,5)+")";
  GLIST=append(GLIST,tmp);
  THETA=(TH.x-xPos)*20*pi/180;
  PHI=(FI.x-xPos)*40*pi/180; // 16.06.20upto
  if(isselected(TH) % isselected(FI), // 17.05.18from
    if(length(Ch)>0,
      ChNum=Ch_(length(Ch));
      if(ChNum==0, ChNum=1);
    ,
      ChNum=1;
    );
    Ch=[0];
  );// 17.05.18upto
  if(length(VLIST)==0, // 16.06.20
    tmp=remove(allpoints(),[NE,SW,TH,FI,O,X,Y,Z]);//17.06.02
    tmp4=remove(tmp,ptexception);
    forall(tmp4,pt,
      tmp1=text(pt);
      tmp=substring(tmp1,length(tmp1)-1,length(tmp1));
      if(tmp!="z",
        tmp=parse(tmp1+"z.y");
        pt3=Xyzcoord(pt.x,pt.y,tmp);
        Defvar(tmp1+"3d",pt3);
        pt2=Parapt(pt3);  // 16.05.28from
        Defvar(tmp1+"2d",pt2);  // 16.05.28upto
      );
    );
  ,
    if(isselected(NE) % isselected(SW), 
      tmp=remove(allpoints(),[NE,SW,TH,FI]);
      tmp4=remove(tmp,ptexception);
      forall(tmp4,pt,
        tmp1=text(pt);
        tmp=substring(tmp1,length(tmp1)-1,length(tmp1));
        if(tmp!="z",
          tmp=select(VLIST,#_1==tmp1+"3d"); //17.10.07
          pt3=tmp_1_2;
          tmp=tmp1+".xy=Parapt("+pt3+")_[1,2];";
          parse(tmp);
//          if(SUBSCR==1,
//            tmp=tmp1+"z"+".x=NE.x-SW.x+"+tmp1+".x";
//            parse(tmp);
//          );
        );
      );
    ,
      tmp=remove(allpoints(),[NE,SW,TH,FI]);
      tmp4=remove(tmp,ptexception);
      forall(tmp4,pt,
        tmp1=text(pt);
        tmp=substring(tmp1,length(tmp1)-1,length(tmp1));
        if(tmp!="z",
          tmp2=parse(tmp1+"z");
          if(isselected(pt) % isselected(tmp2),
            Defvar(tmp1+"2d",pt.xy);
            tmp=parse(tmp1+"z.y");
            tmp=Xyzcoord(pt.x,pt.y,tmp);
            Defvar(tmp1+"3d",tmp);
          );
        );
      );
    );
  );
  if(SUBSCR==1,
    xmn=SW.x+NE.x-SW.x;
    xMx=NE.x+NE.x-SW.x;
    ymn=SW.y;
    yMx=NE.y;
    drawpoly([[xMx,yMx],[xmn,yMx],
      [xmn,ymn],[xMx,ymn]],color->[1,1,1]);
    connect([[NE.x-SW.x,yMx],[NE.x-SW.x,ymn]],
      color->[0.5,0.5,0.5]);
  );
 // for Presentation //17.07.01from
  letterc=[0.98,0.13,0,0.43]; boxc=[0,0.32,0.52,0];
  shadowc=[0,0,0,0.5]; mboxc="yellow"; //17.03.02 regional debugged
  SlideColorList=[letterc,boxc,boxc,boxc,shadowc,shadowc,6,1.3,
                letterc,mboxc,mboxc,mboxc,62,2,letterc];
  ThinDense=0.1; //17.07.01upto
   Ptseg3data(ptexception);  //16.08.23
);

Setangle(theta,phi):=( //16.12.24
//help:Setangle(60,30);
  regional(xmn,xMx,ymn,yMx,pt,pt3,pt2,
    xPos,yTh,yPh,Eps,tmp,tmp1,tmp2,tmp3,tmp4);
  tmp=round(4*SW.y)/4;
  xPos=-5;yTh=tmp-0.5;yPh=tmp-1;
  if(theta=="",theta=THETA*180/pi); // 17.01.19
  if(phi=="",phi=PHI*180/pi);
  THETA=theta*pi/180;
  TH.x=theta/20+xPos;
  PHI=phi*pi/180;
  FI.x=phi/40+xPos;
//  drawtext([xPos-0.8,yTh-0.1],text(theta)); //17.05.05
//  drawtext([xPos-0.8,yPh-0.1],text(phi));
  tmp="Setangle("
    +format(theta,5)+","+format(phi,5)+")";
  GLIST=append(GLIST,tmp);
  if(length(VLIST)==0, // 16.06.20
    tmp=remove(allpoints(),[NE,SW,TH,FI]);
    tmp4=remove(tmp,ptexception);
    forall(tmp4,pt,
      tmp1=text(pt);
      tmp=substring(tmp1,length(tmp1)-1,length(tmp1));
      if(tmp!="z",
        tmp=parse(tmp1+"z.y");
        pt3=Xyzcoord(pt.x,pt.y,tmp);
        Defvar(tmp1+"3d",pt3);
        pt2=Parapt(pt3);  // 16.05.28from
        Defvar(tmp1+"2d",pt2);  // 16.05.28upto
      );
    );
  ,
    if(isselected(NE) % isselected(SW), 
      tmp=remove(allpoints(),[NE,SW,TH,FI]);
      tmp4=remove(tmp,ptexception);
      forall(tmp4,pt,
        tmp1=text(pt);
        tmp=substring(tmp1,length(tmp1)-1,length(tmp1));
        if(tmp!="z",
          tmp=select(VLIST,#_1==tmp1+"3d");
          pt3=tmp_1_2;
          tmp=tmp1+".xy=Parapt("+pt3+")_[1,2];";
          parse(tmp);
        );
      );
    ,
      tmp=remove(allpoints(),[NE,SW,TH,FI]);
      tmp4=remove(tmp,ptexception);
      forall(tmp4,pt,
        tmp1=text(pt);
        tmp=substring(tmp1,length(tmp1)-1,length(tmp1));
        if(tmp!="z",
          tmp2=parse(tmp1+"z");
          if(isselected(pt) % isselected(tmp2),
            Defvar(tmp1+"2d",pt.xy);
            tmp=parse(tmp1+"z.y");
            tmp=Xyzcoord(pt.x,pt.y,tmp);
            Defvar(tmp1+"3d",tmp);
          );
        );
      );
    );
  );
  forall(Datalist3d(),#,
    tmp1=replace(#,"3d","2d");
    tmp2=Projpara(#,["nodata"]);
    tmp=tmp1+"="+textformat(tmp2,5);
    parse(tmp);
  );
  Ptseg3data(ptexception); 
);

Changestyle3d(nameL,style):=(
//help:Changestyle3d(["geoseg3d","ax3d"],["notex"]);
  regional(nmL,name,tmp,tmp1,tmp2);
  if(islist(nameL),nmL=nameL,nmL=[nameL]);
  tmp1=[];
  forall(nmL,name,
    tmp=parse(name);
    if(isstring(tmp_1),
      tmp1=concat(tmp1,tmp);
    ,
      tmp1=append(tmp1,name);
    );
    tmp=apply(tmp1,replace(#,"3d","2d"));
    Changestyle(tmp,style);
    tmp=apply(tmp,"sub"+#); // 15.05.24
    Changestyle(tmp,style);
  );
);

Dist3d(pt1):=Dist3d(pt1,[0,0,0]);
Dist3d(pt1,pt2):=(
//help:Dist3d(A3d,B3d);
//help:Dist3d(A,B);
//help:Dist3d("A","B");
  regional(p1,p2);
  if(islist(pt1),
    p1=pt1;
  ,
    if(ispoint(pt1),p1=text(pt1),p1=pt1);
    p1=parse(p1+"3d");
  );
  if(islist(pt2),
    p2=pt2;
  ,
    if(ispoint(pt2),p2=text(pt2),p2=pt2);
    p2=parse(p2+"3d");
  );
  sqrt((p2-p1)*(p2-p1));
);

Findangle(vec):=(
//help:Findangle([2,1,4]);
//help:Findangle([0,0,1,0]);
  regional(vec3,vec2,Eps,th,ph,tmp);
  Eps=10^(-4);
  vec3=vec_(1..3);
  vec2=vec_(1..2);
  th=arccos(vec_3/|vec3|);
  if(|vec2|>Eps,
    ph=arccos(vec_1/|vec2|);
    if(vec_2<0,
      ph=2*pi-ph; // 15.03.28
    );
  ,
    if(length(vec)>3,ph=vec_4*pi/180,ph=PHI);
  );
  [th,ph];
);

Cancoordpara(pc):=(
//help:Cancoordpara([1,2,0]);
  regional(Xz,Yz,Zz,tmp1,tmp2,tmp3);
  Xz=pc_1; Yz=pc_2; Zz=pc_3;
  tmp1=-Xz*sin(PHI)-Yz*cos(PHI)*cos(THETA)+Zz*cos(PHI)*sin(THETA);
  tmp2=Xz*cos(PHI)-Yz*sin(PHI)*cos(THETA)+Zz*sin(PHI)*sin(THETA);
  tmp3=Yz*sin(THETA)+Zz*cos(THETA);
  [tmp1,tmp2,tmp3];
);

Zparapt(cc):=(
  regional(x,y,z);
  x=cc_1; y=cc_2; z=cc_3;
  x*cos(PHI)*sin(THETA)+y*sin(PHI)*sin(THETA)+z*cos(THETA);
);

Projcoordpara(cc):=(
//help:Projcoordpara([3,1,2]);
  regional(tmp);
  tmp=Parapt(cc);
  [tmp_1,tmp_2,Zparapt(cc)];
);

Parapt(pt):=(
//help:Parapt([2,1,5]);
  regional(Xz,Yz);
  Xz=-pt_1*sin(PHI)+pt_2*cos(PHI);
  Yz=-pt_1*cos(PHI)*cos(THETA)-pt_2*sin(PHI)*cos(THETA)+pt_3*sin(THETA);
  [Xz,Yz];
);

ProjCurve(crv):=(
//help:Projcurve("sl3d1");
  regional(CurveL,Curve,Eps,AnsL,Out,sp,cp,st,,ct,ii,pt,
       Xz,Yz,tmp,tmp1,tmp2);
  Eps=10^(-6);
  sp=sin(PHI); cp=cos(PHI);
  st=sin(THETA); ct=cos(THETA);
  if(isstring(crv),CurveL=parse(crv),CurveL=crv);
  if(MeasureDepth(CurveL)==1,CurveL=[CurveL]);
  Out=[];
  forall(CurveL,Curve,
    AnsL=[];
    forall(1..length(Curve),ii,
      pt=Curve_ii;
      if(pt_1!="inf",
        Xz=-pt_1*sp+pt_2*cp;
        Yz=-pt_1*cp*ct-pt_2*sp*ct+pt_3*st;
        tmp=[Xz,Yz];
        if(ii==1,
           AnsL=[tmp];
        ,
          tmp1=AnsL_(length(AnsL));
          if(tmp1_1=="inf" % |tmp-tmp1|>Eps,
            AnsL=append(AnsL,tmp);
          );
        );
      ,
        AnsL=append(AnsL,["inf"]);
      );
    );
    tmp1=[];
    tmp2=select(1..length(AnsL),AnsL_#==["inf"]);
    tmp=1;
    forall(tmp2,
      if(#>tmp,
        tmp1=concat(tmp1,[AnsL_(tmp..(#-1))])
      );
      tmp=#+1;
    );
    if(tmp<length(AnsL),
      tmp1=concat(tmp1,[AnsL_(tmp..length(AnsL))]);
    );
    AnsL=tmp1;
    if(length(AnsL)==1,AnsL=AnsL_1);
    Out=append(Out,AnsL);
  );
  if(length(Out)==1,
    Out=Out_1;
  );
  Out;
);

Projpara(ptdata):=Projpara(ptdata,[]);
Projpara(ptdata,options):=(
  regional(name2,name3,Ltype,Noflg,opcindy,dtL,ptL,tmp,Out);
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  opcindy=tmp_(length(tmp));
  if(islist(ptdata),dtL=ptdata,dtL=[ptdata]);
  Out=[];
  forall(dtL,name3,
//    name3=#;
    if(indexof(name3,"3d")>0, //17.05.24
      name2=replace(name3,"3d","2d");
    ,
      name2="para"+name3;
    );
    ptL=Projcurve(name3);
    ptL=select(ptL,length(#)>1);//17.06.02
    if(length(ptL)==1,
      ptL=ptL_1;
    );
    Out=append(Out,ptL);
    if(Noflg<3,
      println("generate projparadata  "+name2);
      tmp=name2+"="+textformat(ptL,5);
      parse(tmp);
      tmp=name2+"=Projpara("+name3+")";
      GLIST=append(GLIST,tmp);
    );
    if(Noflg<2,
      if(isstring(Ltype),
        Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
      ,
        if(Noflg==1,Ltype=0);
      );
      GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    );
  );
  if(length(Out)==1,Out=Out_1);
//  Out=Flattenlist(Out);
  if(length(Out)==1,Out=Out_1); // 15.02.24
  tmp=[];
  forall(Out,
    if(length(#)>0,tmp=append(tmp,#));
  );
  Out=tmp;
  Out;
);

InvparaptPp(pt,pd):=(
  regional(Eps,fk,nfk,ph,fh,ah,bh,ak,bk,v1,v2,
    nn,s0,t2,out,tmp,tmp1,tmp2,flg);
  Eps=10^(-4);
  if(isstring(pd),
    fk=pd;
  ,
    fk=textformat(pd,5);
  );
  nfk=Numptcrv(parse(fk));
  flg=0;
  tmp=pt;
  if(length(tmp)==1,
    ph=tmp;
    fh=pd;
  ,
    fh=Projpara(fk,["nodata"]);
    if(nfk>2,
      tmp1=Nearestpt(tmp,fh);
      ph=tmp1_2;
    ,
      ah=Ptcrv(1,fh); bh=Ptcrv(2,fh);
      v1=tmp-ah; v2=bh-ah;
      tmp1=Crossprod(v1,v2);
      if(abs(tmp1)>Eps,
        println("  Not on the line");
        out=[];
        flg=1;
      ,
        ph=Dotprod(v1,v2)/|v2|^2+1;
      );
    );
  );
  if(flg==0,
    if(nfk>2,
      nn=floor(ph);
      s0=ph-nn;
      if(ph>Numptcrv(fh)-Eps,
        out=[Ptend(fk),Numptcrv(fh)];
        flg=1;
      );
    ,
      nn=1;
      s0=ph-1;
    );
  );
  if(flg==0,
    ak=Ptcrv(nn,fk); bk=Ptcrv(nn+1,fk);
    ah=Ptcrv(nn,fh); bh=Ptcrv(nn+1,fh);
    ph=(1-s0)*ah+a0*bh;
    t2=s0;
    pk=(1-t2)*ak+t2*bk;
    out=[pk,nn+t2];
  );
  out;
);

Invparapt(pt,pd):=(
  regional(tmp);
  tmp=InvparaptPp(pt,pd);
  tmp_1;
);

Subgraph(name,opcindy):=(
  regional(tht,tmp,tmp1);
  tht=THETA; THETA=pi/2;
  tmp=Projpara(name3,["nodata"]);
  if(length(tmp)>0, //15.08.17
    tmp=Translatedata("",[tmp],[NE.x-SW.x,0],["nodata"]);
    tmp1=replace("sub"+name,"3d","2d");
    tmp=tmp1+"="+Textformat(tmp,5);
    parse(tmp);
    GCLIST=append(GCLIST,[tmp1,0,opcindy]);
  );
  THETA=tht;  // 15.11.03
);

Spaceline(ptlist):=Spaceline(ptlist,[]); // 16.02.22 from
Spaceline(Arg1,Arg2):=(
  regional(name,tmp,tmp1,tmp2);
  if(isstring(Arg1),
    Spaceline(Arg1,Arg2,[]);
  ,
    tmp=text(Arg1);
    tmp=substring(tmp,1,length(tmp)-1);
    name="-"+replace(tmp,",","");
    Spaceline(name,Arg1,Arg2);
  );
); // 16.02.22 upto
Spaceline(nm,ptlistorg,optionorg):=(
//help:Spaceline("1",[[2,5,1],[4,2,3]]);
//help:Spaceline([A,B]);
  regional(name2,name3,options,Out,tmp,tmp1,tmp2,
        opstr,opcindy,Ltype,Noflg,eqL,ptlist, Msg);
  ptlist=apply(ptlistorg,if(ispoint(#),parse(text(#)+"3d"),#)); // 16.02.10
  if(substring(nm,0,2)=="bz",
    name2=replace(nm,"bz","bz2d");
    name3=replace(nm,"bz","bz3d");
  ,
    if(substring(nm,0,1)=="-",
      tmp=substring(nm,1,length(nm));
      if(indexof(tmp,"3d")>0,name3=tmp,name3=tmp+"3d");
      name2=replace(name3,"3d","2d");
    ,
      name2="sl2d"+nm;
      name3="sl3d"+nm;
    );
  );
  options=select(optionorg,length(#)>0);  // 160415
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  eqL=tmp_5;
  opcindy=tmp_(length(tmp));
  Msg=1;
  forall(eqL,
    tmp=substring(#,0,1);
    if(Toupper(tmp)=="M",
      tmp=indexof(#,"=");
      tmp1=substring(#,tmp,tmp+1);
      if(Toupper(tmp1)=="N",
        Msg=0;
      );
    );
  );
  if(Noflg<3,
    if(Msg==1,
      println("generate Spaceline "+name3);
    );
    tmp=name3+"="+ptlist;
    parse(tmp);
    tmp1=Projpara(name3,["nodata"]);
    tmp=name2+"="+textformat(tmp1,5);
    parse(tmp);
    tmp="[";
    forall(ptlist, //17.07.13from
      if(isstring(#),
        tmp=tmp+#+",";
      ,
        tmp=tmp+textformat(#,5)+",";
      );
    );
    tmp=substring(tmp,0,length(tmp)-1)+"]";
    tmp=name3+"=Spaceline("+tmp+")"; //17.07.13upto
//    tmp=name3+"=Spaceline("+textformat(ptlist,5)+")"; //17.05.24
    GLIST=append(GLIST,tmp);
    tmp=name2+"=Projpara("+name3+")";
    GLIST=append(GLIST,tmp);
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    if(SUBSCR==1, //  15.02.11
      Subgraph(name3,opcindy);
    );
  );
  ptlist;
);

Spacecurve(nm,funstr,variable):=Spacecurve(nm,funstr,variable,[]);
Spacecurve(nm,funstr,variable,optionorg):=(
//help:Spacecurve("1","[cos(t),sin(t),0.5*t]","t=[0,4*pi]",["Num=200"]);
  regional(name2,name3,options,Out,tmp,tmp1,tmp2,vname,tmpfn,str,Rng,Num,Msg,
     Ec,Exfun,Dc,opstr,opcindy,Fntmp,Vatmp,Ltype,Noflg,eqL,t1,t2,dt,tt,pa,ke);
  if(substring(nm,0,2)=="bz",
    name2=replace(nm,"bz","bz2d");
    name3=replace(nm,"bz","bz3d");
  ,
    name2="sc2d"+nm;
    name3="sc3d"+nm;
  );
  Eps=10^(-4);
  Num=50;
  Ec=[];
  Exfun="";
  Dc=1000;
  options=select(optionorg,length(#)>0);  // 160415
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  eqL=tmp_5;
  opstr=tmp_(length(tmp)-1);
  opcindy=tmp_(length(tmp));
  Msg=1;
  forall(eqL,
    tmp=substring(#,0,1);
    if(Toupper(tmp)=="M",
      tmp=indexof(#,"=");
      tmp1=substring(#,tmp,tmp+2);
      if(Toupper(tmp1)=="NO",
        Msg=0;
      );
    );
    tmp=indexof(#,"N=");
    if(tmp>0,
      tmp1=substring(#,tmp+1,length(#));
      Num=parse(tmp1);
      opstr=opstr+",'"+#+"'";
    );
    tmp=indexof(#,"Num=");
    if(tmp>0,
      tmp1=substring(#,tmp+3,length(#));
      Num=parse(tmp1);
      opstr=opstr+",'"+#+"'";
    );
    tmp=indexof(#,"E=");
    if(tmp>0,
      tmp1=substring(#,tmp+1,length(#));
      if(substring(tmp1,0,1)=="[",
        Ec=parse(tmp1);
      ,
        Exfun=tmp1;
      );
      opstr=opstr+",'"+#+"'";
    );
    tmp=indexof(#,"Exc=");
    if(tmp>0,
      tmp1=substring(#,tmp+3,length(#));
      if(substring(tmp1,0,1)=="[",
        Ec=parse(tmp1);
      ,
        Exfun=tmp1;
      );
    );
  );
  tmp=indexof(variable,"=");
  vname=substring(variable,0,tmp-1);
  str=substring(variable,tmp,length(variable));
  Rng=parse(str);
  t1=Rng_1; t2=Rng_2;
  dt=(t2-t1)/Num;  // differ from Scilab
  tmp="tmpfn("+vname+"):="+funstr+";";
  parse(tmp);
  Out=[];
  Ec=append(sort(Ec),10000);
  ke=1;
  forall(0..Num, 
    pt=[];
    tt=Rng_1+#*dt;
    if(tt-Ec_ke<-Eps,
      pa=tmpfn(tt);
    );
    if(abs(tt-Ec_ke)<=Eps,
      if(length(Out)>0,
        if(Out_(length(Out))_1!="inf",
          pa=["inf"];
        );
      );
    );
    if(tt-Ec_ke>Eps,
      pa=tmpfn(tt);
      ke=ke+1;
    );
    if(length(pa)>0,
      if(pa_1=="inf",
        Out=append(Out,pa);
      ,
        if(length(Out)==0,
          Out=[pa];
        ,
          tmp=Out_(length(Out));
          if(tmp_1=="inf",
            Out=append(Out,pa);
          ,
            if(|tmp-pa|<Dc,
              Out=append(Out,pa);
            ,
              Out=concat(Out,[["inf"],pa]);
            );
          );
        );
      );
    );
  );
  tmp1=[];
  tmp2=select(1..length(Out),Out_#==["inf"]);
  tmp=1;
  forall(tmp2,
    if(#>tmp,
      tmp1=concat(tmp1,[Out_(tmp..(#-1))])
    );
    tmp=#+1;
  );
  if(tmp<length(Out),
    tmp1=concat(tmp1,[Out_(tmp..length(Out))]);
  );
  if(length(Out)==1,
    Out=Out_1;
  );
  if(Noflg<3,
    if(Msg==1,
      println("generate Spacecurve "+name3);
    );
    tmp=name3+"="+textformat(Out,5);
    parse(tmp);
    tmp1=Projpara(name3,["nodata"]);
    tmp=name2+"="+textformat(tmp1,5);
    parse(tmp);
    tmp=name3+"=Spacecurve(Assign('"+funstr+"'),'"+variable+"'"+opstr+")";
    GLIST=append(GLIST,tmp);
    tmp=name2+"=Projpara("+name3+")";
    GLIST=append(GLIST,tmp);
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    if(SUBSCR==1, //  15.02.11
      tmp=parse(name2);  // 15.08.17
      if(length(tmp)>0,
        Subgraph(name3,opcindy);
      );   // 15.08.17
    );
  );
  Out;
);

Partcrv3d(nm,pA,pB,PkLstr):=Partcrv3d(nm,pA,pB,PkLstr,[]);
Partcrv3d(nm,pA,pB,PkLstr,options):=(
//help:Partcrv3d("1",A,B,"sl3d1");
//help:Partcrv3d("1",1.3,2.5,"sl3d1");
  regional(p1,p2,q1,q2,dt1,dts,dt3,tmp,tmp1,tmp2);
  if(isreal(pA),
    p1=pA; q1=pA;
  ,
    if(islist(pA),
      tmp=pA;
    ,
      if(ispoint(pA),tmp=text(pA),tmp=pA);
      tmp=parse(tmp+"3d");
    );
    p1=parapt(tmp);
    q1=[p1_1,tmp_3]+[NE.x-SW.x,0];
  );
  if(isreal(pB),
    p2=pB; q2=pB;
  ,
    if(islist(pB),
      tmp=pB;
    ,
      if(ispoint(pB),tmp=text(pB),tmp=pB);
      tmp=parse(tmp+"3d");
    );
    p2=parapt(tmp);
    q2=[p2_1,tmp_3]+[NE.x-SW.x,0];
  );
  tmp=replace(PkLstr,"3d","2d");
  dt1=partcrv("",p1,p2,tmp,["nodata"]);
  dts=partcrv("",q1,q2,"sub"+tmp,["nodata"]);
  dt=apply(1..length(dt1),
    Xyzcoord(dt1_#_1,dt1_#_2,dts_#_2));
  Spaceline("-part3d"+nm,dt,options);
);

Joincrvs3d(nm,plotstrL):=Joincrvs3d(nm,plotstrL,[]);//16.10.06
Joincrvs3d(nm,plotstrL,options):=(
//help:Joincrvs3d("1",["bz3da1","bz3da1"]);
  regional(PtL,Eps,QdL,Flg,Ni,Qd,pP,pS,pQ,pR,rMN,
        opcindy,tmp,tmp1,tmp2,str,name2,name3,Ltype,Noflg);
  name2="join2d"+nm;
  name3="join3d"+nm;
  QdL=[];
  forall(plotstrL,str,
    if(isstring(str),
      tmp=parse(str);
    ,
      tmp=str;
    );
    QdL=append(QdL,tmp);
  );
  Eps=10^(-4);
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  opcindy=tmp_(length(tmp));
  tmp1=tmp_6;
  if(length(tmp1)>0,Eps=tmp1_1);
  Flg=0;
  if(length(QdL)==0,
    PtL=[];
    Flg=1;
  );
  if(Flg==0,
    PtL=QdL_1;
    forall(2..length(QdL),Ni,
      Qd=QdL_Ni;
      if(length(Qd)>1,
        pP=PtL_1;
        pS=PtL_(length(PtL));
        pQ=Qd_1;
        pR=Qd_(length(Qd));
        rMN=min([norm(pP-pQ),norm(pP-pR),
              norm(pS-pQ),norm(pS-pR)]);
        if(rMN==norm(pP-pR),
          Qd=reverse(Qd);
        ,
          if(rMN==norm(pS-pQ),
            PtL=reverse(PtL);
          ,
            if(rMN==norm(pS-pR),
              PtL=reverse(PtL);
              Qd=reverse(Qd);
            );
          );
        );
      );
      if(rMN>Eps,
        PtL=concat(PtL,Qd);
      ,
        PtL=concat(PtL,Qd_(2..length(Qd)));
      );
    );
  );
  if(Noflg<3,
    println("generate Joincurves3d "+name3);
    tmp=name3+"="+textformat(PtL,5);
    parse(tmp);
    tmp1=Projpara(name3,["nodata"]);
    tmp=name2+"="+textformat(tmp1,5);
    parse(tmp);
    tmp=text(plotstrL);
    tmp=substring(tmp,1,length(tmp)-1);
    tmp=name3+"=Joincrvs("+tmp+")";
    GLIST=append(GLIST,tmp);
    tmp=name2+"=Projpara("+name3+")";
    GLIST=append(GLIST,tmp);
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    if(SUBSCR==1, //  15.02.11
      tmp=parse(name2);  // 15.08.17
      if(length(tmp)>0,
        Subgraph(name3,opcindy);
      );   // 15.08.17
    );
  );
  PtL;
);

Xyzax3data(nm,Xrange,Yrange,Zrange):=
          Xyzax3data(nm,Xrange,Yrange,Zrange,[]);
Xyzax3data(nm,Xrange,Yrange,Zrange,options):=(
//help:Xyzax3data("","x=[-5,5]","y=[-5,5]","z=[-5,5]");
//help:Xyzax3data(Options2=["a1","Osw"]);
  regional(name2,name3,Out,tmp,tmp1,tmp2,eqL,reL,strL,
    opstr,opcindy,Ltype,Noflg,Axname,Arrow,Origin);
  name2="ax2d"+nm;
  name3="ax3d"+nm;
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  eqL=tmp_5;
  reL=tmp_6;
  strL=tmp_7;
  opcindy=tmp_(length(tmp));
  Axname=1;
  Arrow=0;  // 16.08.14
  Origin=""; // 16.08.14
  if(length(reL)>0,
    Axname=reL_1;
  );
  forall(strL,  // 16.08.14from
    tmp1=Toupper(substring(#,0,1));
    if(tmp1=="A",
      tmp=substring(#,1,length(#));
      if(length(tmp)>0,
        Arrow=parse(tmp);
      ,
        Arrow=1;
      );
    );
    if(tmp1=="O",
      tmp=indexof(#,",");
      if(tmp>0,
        Origin=substring(#,tmp,length(#));
      ,
        tmp=substring(#,1,length(#));
        if(length(tmp)>0,
          Origin=tmp;
        ,
          Origin="sw";
        );
      );
    );
  ); // 16.08.14upto
  tmp1=indexof(Xrange,"=");
  tmp=parse(substring(Xrange,tmp1,length(Xrange)));
  Px=[tmp_1,0,0]; Qx=[tmp_2,0,0];
  tmp1=indexof(Yrange,"=");
  tmp=parse(substring(Yrange,tmp1,length(Yrange)));
  Py=[0,tmp_1,0]; Qy=[0,tmp_2,0];
  tmp1=indexof(Zrange,"=");
  tmp=parse(substring(Zrange,tmp1,length(Zrange)));
  Pz=[0,0,tmp_1]; Qz=[0,0,tmp_2];
  Out=[[Px,Qx],[Py,Qy],[Pz,Qz]];
  if(Axname>0,
    Xyzaxparaname(Xrange,Yrange,Zrange,[Axname]);
  );
  if(Noflg<3,
    println("generate axes "+name3);
    tmp=name3+"="+textformat(Out,5);
    parse(tmp);
    tmp1=Projpara([name3],["nodata"]);
    tmp=name2+"="+textformat(tmp1,5);
    parse(tmp);
    tmp=name3+"=Xyzax3data("+
	  Dq+Xrange+Dq+","+Dq+Yrange+Dq+","+Dq+Zrange+Dq+")";
    GLIST=append(GLIST,tmp);
    tmp=name2+"=Projpara("+name3+")";
    GLIST=append(GLIST,tmp);
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    if(Noflg<1,  // 16.08.14from
      if(Arrow>0,
        tmp1=Parapt(Px); tmp2=Parapt(Qx);
        Arrowhead(tmp2,tmp2-tmp1,[Arrow]);
        tmp1=Parapt(Py); tmp2=Parapt(Qy);
        Arrowhead(tmp2,tmp2-tmp1,[Arrow]);
        tmp1=Parapt(Pz); tmp2=Parapt(Qz);
        Arrowhead(tmp2,tmp2-tmp1,[Arrow]);
      );
      if(length(Origin)>0,
        Letter([Parapt([0,0,0]),Origin,"O"]);
      );
    );  // 16.08.14upto
    if(SUBSCR==1, //  15.02.11
      Subgraph(name3,opcindy);
    );
  );
  Out;
);

Xyzaxparaname(Xrange,Yrange,Zrange):=
   Xyzaxparaname(Xrange,Yrange,Zrange,[]);
Xyzaxparaname(Xrange,Yrange,Zrange,options):=(
 regional(tmp,tmp1,tmp2,Eps,Dr,Noflg,Xname,Yname,Zname,
     px,qx,py,qy,pz,qz,ph,qh,rr,ch);
  Eps=10.0^(-6);
  Dr=0.19*1000/2.54/MilliIn;
  Noflg=0;
  forall(options,
    if(isreal(#),Dr=Dr*#);
  );
  tmp=indexof(Xrange,"=");
  Xname=substring(Xrange,0,tmp-1);
  tmp=parse(substring(Xrange,tmp,length(Xrange)));
  px=[tmp_1,0,0]; qx=[tmp_2,0,0];
  tmp=indexof(Yrange,"=");
  Yname=substring(Yrange,0,tmp-1);
  tmp=parse(substring(Yrange,tmp,length(Yrange)));
  py=[0,tmp_1,0]; qy=[0,tmp_2,0];
  tmp=indexof(Zrange,"=");
  Zname=substring(Zrange,0,tmp-1);
  tmp=parse(substring(Zrange,tmp,length(Zrange)));
  pz=[0,0,tmp_1]; qz=[0,0,tmp_2];
  ph=Parapt(px); qh=Parapt(qx); rr=|ph-qh|;
  if(rr>Eps,
    ch=qh+Dr/rr*(qh-ph);
    Expr(ch,"c",Xname);
  );
  ph=Parapt(py); qh=Parapt(qy); rr=|ph-qh|;
  if(rr>Eps,
    ch=qh+Dr/rr*(qh-ph);
    Expr(ch,"c",Yname);
  );
  ph=Parapt(pz); qh=Parapt(qz); rr=|ph-qh|;
  if(rr>Eps,
    ch=qh+Dr/rr*(qh-ph);
    Expr(ch,"c",Zname);
  );
);

Datalist3d():=(
//help:Datalist3d();
  regional(out,tmp,tmp2,tmp3);
  tmp=apply(GCLIST,#_1);
  tmp=select(tmp,indexof(#,"2d")>0);
  tmp2=select(tmp,indexof(#,"sub")==0);
  tmp3=apply(tmp2,replace(#,"2d","3d"));
  out=tmp3;
);

Datalist2d():=(
//help:Datalist2d();
  regional(out,tmp,tmp2,tmp3);
  tmp=apply(GCLIST,#_1);
  tmp=select(tmp,indexof(#,"2d")>0);
  tmp2=select(tmp,indexof(#,"sub")==0);
//  tmp3=apply(tmp2,replace(#,"2d","3d"));
  out=tmp2;
);

Embed(nm,Pd2str,funstr,varstr):=
    Embed(nm,Pd2str,funstr,varstr,[]);
Embed(nm,Pd2str,funstr,varstr,options):=(
//help:Embed("1",["gr1"],"A3d+x*(B3d-A3d)+y*(C3d-A3d)","[x,y]");
  regional(name2,name3,Pd2L,Pd2,tmp,tmp1,xstr,ystr,
     Ltype,Noflg,opstr,opcindy,Out);
  name2="em2d"+nm;
  name3="em3d"+nm;
  if(!islist(Pd2str),Pd2L=[Pd2str],Pd2L=Pd2str); // 15.03.06
  tmp1=[];   // 15.10.31
  forall(Pd2L,
    if(isstring(#),tmp=parse(#),tmp=#);
    if(MeasureDepth(tmp)==2,
      tmp1=concat(tmp1,tmp);
    );
    if(MeasureDepth(tmp)==1,
      tmp1=append(tmp1,tmp);
    );
  );
  Pd2L=tmp1;
  tmp=substring(varstr,0,1);
  if(tmp=="[" % tmp=="(",
    tmp1=substring(varstr,1,length(varstr)-1);
  ,
    tmp1=varstr;
  );
  tmp=indexof(tmp1,",");
  xstr=substring(tmp1,0,tmp-1);
  ystr=substring(tmp1,tmp,length(tmp1));
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  opcindy=tmp_(length(tmp));
  Out=[];
  forall(Pd2L,Pd2,
    tmp1=[];
    forall(Pd2,
      tmp=Assign(funstr,xstr,"("+textformat(#_1,5)+")");
      tmp=Assign(tmp,ystr,"("+textformat(#_2,5)+")");
      tmp=parse(tmp);
      tmp1=append(tmp1,tmp);
    );
    Out=append(Out,tmp1);
  );
  if(length(Out)==1,
    Out=Out_1;
  );  
  if(Noflg<3,
    println("generate Embed "+name3);
    if(length(Out)>0,
      tmp=name3+"="+textformat(Out,5);
      parse(tmp);
      tmp1=Projpara(name3,["nodata"]);
      tmp=name2+"="+textformat(tmp1,5);
      parse(tmp);
    );
    if(!islist(Pd2str),
       tmp=Pd2str;
    ,
       tmp=text(Pd2str);
       tmp="list("+substring(tmp,1,length(tmp)-1)+")";
    );
    tmp=name3+"=Embed("+tmp+",'"+funstr+"','"+varstr+"'"+")";
    GLIST=append(GLIST,tmp);
    tmp=name2+"=Projpara("+name3+")";
    GLIST=append(GLIST,tmp);
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    if(length(Out)>0,
      GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
      if(SUBSCR==1, //  15.02.11
        Subgraph(name3,opcindy);
      );
    );
  );
  Out;
);

Rotatept3d(point,w1,w2):=Rotate3pt(point,w1,w2);
Rotatept3d(point,w1,w2,center):=Rotate3pt(point,w1,w2,center);
//help:Rotatept3d(pt3d,[0,0,1],pi/2);
//help:Rotatept3d(pt3d,[0,0,1],pi/2,[1,1,1]);
Rotate3pt(point,w1,w2):=Rotate3pt(point,w1,w2,[0,0,0]);
Rotate3pt(point,w1,w2,center):=(
//help:Rotate3pt(pt3d,[0,0,1],pi/2);
//help:Rotate3pt(pt3d,[0,0,1],pi/2,[1,1,1]);
  regional(Eps,ct,st,v3,v1,v2,Ans,ns,PtL,num,
    pt,tmp,tmp1,tmp2,Retflg,x,y,z,xx,yy,zz,flg);
  Eps=10^(-4);
  Retflg=0;
  if(length(w2)==1,
    ct=cos(w2);
    st=sin(w2);
    v3=1/|w1|*w1;
    if(v3_1==0,
      tmp=[1,0,0];
    ,
      tmp=[0,1,0];
    );
    w1=[tmp_2*v3_3-tmp_3*v3_2,
         tmp_3*v3_1-tmp_1*v3_3,
         tmp_1*v3_2-tmp_2*v3_1];
    v1=1/|w1|*w1;
    v2=[v3_2*v1_3-v3_3*v1_2,
         v3_3*v1_1-v3_1*v1_3,
         v3_1*v1_2-v3_2*v1_1];
  ,
    tmp=[w1_2*w2_3-w1_3*w2_2,
          w1_3*w2_1-w1_1*w2_3,
          w1_1*w2_2-w1_2*w2_1];
    if(|tmp|<Eps,
      Ans=point;
      Retflg=1;
    );
    if(Retflg==0,
      v1=1/|w1|*w1;
      ns=v1_1*w2_1+v1_2*w2_2+v1_3*w2_3;
      tmp=w2-ns*v1;
      v2=1/|tmp|*tmp;
      tmp=[v1_2*v2_3-v1_3*v2_2,
                 v1_3*v2_1-v1_1*v2_3,
                 v1_1*v2_2-v1_2*v2_1];
      v3=1/|tmp|*tmp;
      ct=ns/|w2|;
      st=sqrt(1-ct^2);
    );
  );
  if(Retflg==0,
    if(|tmp|<Eps,
      Ans=point;
      Retflg=1;
    );
  );
  if(Retflg==0,
    if(MeasureDepth(point)>0,
      PtL=point;
    ,
      PtL=[point];
    );
    Ans=[];
    flg=0;
    forall(1..length(PtL),num,
      pt=PtL_num;
      if(pt_1=="inf",
        Ans=append(Ans,["inf"]);
        flg=1;
      );
      if(flg==0,
        x=pt_1-center_1; y=pt_2-center_2; z=pt_3-center_3;
        xx=((v1_1*ct+v2_1*st)*v1_1+ (-v1_1*st+v2_1*ct)*v2_1+v3_1^2)*x
            +((v1_1*ct+v2_1*st)*v1_2+(-v1_1*st+v2_1*ct)*v2_2+v3_1*v3_2)*y
            +((v1_1*ct+v2_1*st)*v1_3+(-v1_1*st+v2_1*ct)*v2_3+v3_1*v3_3)*z; 
        yy=((v1_2*ct+v2_2*st)*v1_1+(-v1_2*st+v2_2*ct)*v2_1+v3_1*v3_2)*x
            +((v1_2*ct+v2_2*st)*v1_2+(-v1_2*st+v2_2*ct)*v2_2+v3_2^2)*y
            +((v1_2*ct+v2_2*st)*v1_3+(-v1_2*st+v2_2*ct)*v2_3+v3_2*v3_3)*z;
        zz=((v1_3*ct+v2_3*st)*v1_1+(-v1_3*st+v2_3*ct)*v2_1+v3_1*v3_3)*x
            +((v1_3*ct+v2_3*st)*v1_2+(-v1_3*st+v2_3*ct)*v2_2+v3_2*v3_3)*y
            +((v1_3*ct+v2_3*st)*v1_3+(-v1_3*st+v2_3*ct)*v2_3+v3_3^2)*z;
        Ans=append(Ans,center+[xx,yy,zz]);
      );
    );
    if(length(Ans)==1,
      Ans=Ans_1;
    );
  );
  Ans;
);

Rotatedata3d(nm,P3data,w1,w2):=Rotate3data(nm,P3data,w1,w2);
Rotatedata3d(nm,P3data,w1,w2,options):=
    Rotate3data(nm,P3data,w1,w2,options);
//help:Rotatedata3d("1",["sl3d1","sc3d2"],[0,0,1],pi/3);
Rotate3data(nm,P3data,w1,w2):=Rotate3data(nm,P3data,w1,w2,[]);
Rotate3data(nm,P3data,w1,w2,options):=(
  regional(name3,name2,center,pdata,Pd3,Pd,Out,tmp,tmp1,
       Ltype,Noflg,opcindy,opstr);
  name3="rot3d"+nm;
  name2="rot2d"+nm;
  center=[0,0,0];
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  opstr=tmp_(length(tmp)-1);
  opcindy=tmp_(length(tmp));
  tmp1=tmp_6;
  if(length(tmp1)>0,
    center=tmp1_1;
  );
  if(islist(P3data) & isstring(P3data_1),Pd3=P3data,Pd3=[P3data]);
  Out=[];
  forall(Pd3,Pd,
    if(isstring(Pd),Pd=parse(Pd));
    if(MeasureDepth(Pd)==1,Pd=[Pd]);
    Ans=[];
    forall(Pd,
      tmp=Rotate3pt(#,w1,w2,center);
      Ans=append(Ans,tmp);
    );
    Out=append(Out,Ans);
  );
  Out=Flattenlist(Out);
  if(length(Out)==1,Out=Out_1);
  if(Noflg<3,
    println("generate Rotate3data "+name3);
    tmp=name3+"="+textformat(Out,5);
    parse(tmp);
    tmp1=Projpara(name3,["nodata"]);
    tmp=name2+"="+textformat(tmp1,5);
    parse(tmp);
    tmp=text(P3data);
    tmp=replace(tmp,"[","list(");
    tmp=replace(tmp,"]",")");
    tmp=name3+"=Rotate3data("+tmp+","
	       +textformat(w1,5)+","+textformat(w2,5)+opstr+")";
    GLIST=append(GLIST,tmp);
    tmp=name2+"=Projpara("+name3+")";
    GLIST=append(GLIST,tmp);
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    if(SUBSCR==1, //  15.02.11
      Subgraph(name3,opcindy);
    );
  );
  Out;
);

Translatept3d(point,w1):=Translate3pt(point,w1);
//help:Translatept3d(pt3d,[1,2,3]);
Translate3pt(point,w1):=(
  regional(Eps,Ans,PtL,pt,num,xx,yy,zz,flg);
  Eps=10^(-4);
  if(MeasureDepth(point)>0,
    PtL=point;
  ,
    PtL=[point];
  );
  Ans=[];
  flg=0;
  forall(1..length(PtL),num,
    pt=PtL_num;
    if(pt_1=="inf",
      Ans=append(Ans,["inf"]);
      flg=1;
    );
    if(flg==0,
      xx=pt_1+w1_1; yy=pt_2+w1_2; zz=pt_3+w1_3;
      Ans=append(Ans,[xx,yy,zz]);
    );
  );
  if(length(Ans)==1,
    Ans=Ans_1;
  );
  Ans;
);

Translatedata3d(nm,P3data,w1):=Translate3data(nm,P3data,w1);
Translatedata3d(nm,P3data,w1,options):=
    Translate3data(nm,P3data,w1,options);
//help:Translatedata3d("1",["sl3d1"],[1,2,3]);
Translate3data(nm,P3data,w1):=Translate3data(nm,P3data,w1,[]);
Translate3data(nm,P3data,w1,options):=(
  regional(name3,name2,pdata,Pd3,Pd,Out,tmp,tmp1,
      Ltype,Noflg,opcindy);
  name3="tra3d"+nm;
  name2="tra2d"+nm;
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  opcindy=tmp_(length(tmp));
  if(islist(P3data) & isstring(P3data_1),Pd3=P3data,Pd3=[P3data]);
  Out=[];
  forall(Pd3,Pd,
    if(isstring(Pd),Pd=parse(Pd));
    if(MeasureDepth(Pd)==1,Pd=[Pd]);
    Ans=[];
    forall(Pd,
      tmp=Translate3pt(#,w1);
      Ans=append(Ans,tmp);
    );
    Out=append(Out,Ans);
  );
  Out=Flattenlist(Out);
  if(length(Out)==1,Out=Out_1);
  if(Noflg<3,
    println("generate Translate3data "+name3);
    tmp=name3+"="+textformat(Out,5);
    parse(tmp);
    tmp1=Projpara(name3,["nodata"]);
    tmp=name2+"="+textformat(tmp1,5);
    parse(tmp);
    tmp=text(P3data);
    tmp=replace(tmp,"[","list(");
    tmp=replace(tmp,"]",")");
    tmp=name3+"=Translate3data("+tmp+","+textformat(w1,5)+")";
    GLIST=append(GLIST,tmp);
    tmp=name2+"=Projpara("+name3+")";
    GLIST=append(GLIST,tmp);
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    if(SUBSCR==1, //  15.02.11
      Subgraph(name3,opcindy);
    );
  );
  Out;
);

Reflectpoint3d(point,vecL):=Reflect3point(point,vecL);
Reflectpt3d(point,vecL):=Reflect3pt(point,vecL);
//help:Reflectpoint3d(pt3d,[v1,v2,v3]);
Reflect3point(point,vecL):=Reflect3pt(point,vecL);
Reflect3pt(point,vecL):=(
  regional(ans,v1,v2,v3,tmp,tmp1);
  if(length(vecL)==1,
    v1=vecL_1;
    ans=2*v1-point;
  );
  if(length(vecL)==2,
    v1=vecL_1;
    v2=vecL_2;
    v3=(v2-v1)/|v2-v1|;
    tmp=v1+Dotprod(point-v1,v3)*v3;
    ans=2*tmp-point;
  );
  if(length(vecL)==3,
    v1=vecL_1;
    v2=vecL_2;
    v3=vecL_3;
    tmp=Crossprod(v2-v1,v3-v1);
    tmp1=point-Dotprod(tmp,point-v1)/|tmp|^2*tmp;
    ans=2*tmp1-point;
  );
  ans;
);

Xyzcoord(X,Y,z):=(
//help:Xyzcoord(A.x,A.y,Az.y);
  regional(Eps,x3d,y3d,z3d);
  x3d=(-X*sin(PHI)*cos(THETA)-Y*cos(PHI)+z*cos(PHI)*sin(THETA))/cos(THETA);
  y3d=(X*cos(PHI)*cos(THETA)-Y*sin(PHI)+z*sin(PHI)*sin(THETA))/cos(THETA);
  z3d=z;
  [x3d,y3d,z3d];
);

PutonCurve3d(name,pdstr):=(
//help:PutonCurve3d("T","sc3d1");
  regional(pt,pd2str,tmp,tmp1,tmp2);
  pd2str=replace(pdstr,"3d","2d");
  PutonCurve(name,pd2str);
  pt=parse(name+".xy");
  tmp=Nearestpt(pt,pd2str);  // 15.03.13
  tmp1=ParamonCurve(tmp_1,tmp_2,pd2str);
  tmp="sub"+pd2str;
  tmp2=PointonCurve(tmp1,tmp);
  pt=append(pt,tmp2_2);
  pt=Xyzcoord(pt_1,pt_2,pt_3); // 15.03.13
  tmp=name+"z.xy="+textformat(tmp2,5);
  parse(tmp);
  VLIST=select(VLIST,#_1!=name+"3d");
  Defvar(name+"3d",pt);
);

Mkpointlist():=Mkpointlist([]); //16.11.12
Mkpointlist(options):=( //16.11.12
  regional(Eps,pos,ptL,plist,ilist,plistrest,
    inforest,free,pt,pt3,ptz,par,vlistpre,
     tmp,tmp1,tmp2,tmp3,tmp4,p1,p2,flg);
  Eps=10^(-4);
  tmp=Finddef(NE);
  free=tmp_1;
  pos=[NE.x+1,NE.y];
  tmp=remove(allpoints(),[NE,SW,TH,FI]);
  tmp=remove(tmp,options); //16.11.12
  plist=select(tmp,
    substring(text(#),length(text(#))-1,length(text(#)))!="z"
  );
  if(SUBSCR==0,plist=[]);
  ptL=[];
  plistrest=[];
  inforest=[];
  forall(plist,pt,
    inspect(pt,"ptsize",3);
    inspect(pt,"color",2);
    inspect(pt,"textsize",TSIZE);
    ptz=text(pt)+"z";
    tmp=Finddef(pt);
    if(indexof(tmp_1,free)>0,  // free point
	  tmp=select(VLIST,#_1==text(pt)+"3d");
      if(length(tmp)==0,
        tmp=pt.x+NE.x-SW.x;
        Putpoint(ptz,[tmp,0],[tmp,parse(ptz+".y")]);
        pt3=Xyzcoord(pt.x,pt.y,parse(text(ptz)+".y"));
        Defvar(text(pt)+"3d",pt3);
        Defvar(text(pt)+"fix",0);
        vlistpre=VLIST; //16.08.20
      );
      flg=0;
      if(isselected(pt),
        if(parse(text(pt)+"fix")!=1, // 15.03.01
          tmp=pt.x+NE.x-SW.x;
          Putpoint(ptz,[tmp,0],[tmp,parse(ptz+".y")]);
          pt3=Xyzcoord(pt.x,pt.y,parse(text(ptz)+".y"));
          VLIST=select(VLIST,#_1!=text(pt)+"3d");
          Defvar(text(pt)+"3d",pt3);
          vlistpre=VLIST; //16.08.20
          flg=1;
        );
      );
      inspect(parse(ptz),"ptsize",3);
      inspect(parse(ptz),"color",3);
      inspect(parse(ptz),"textsize",TSIZEZ);
      if(isselected(parse(ptz)),
        if(parse(ptz+"fix")!=1, // 15.03.01
          tmp=select(vlistpre,#_1==text(pt)+"3d"); //16.08.20
          pt3=tmp_1_2;
          pt3_3=parse(ptz+".y");
          pt.xy=Parapt(pt3);
          tmp=ptz+".x=pt.x+NE.x-SW.x";
          parse(tmp);
          VLIST=select(VLIST,#_1!=text(pt)+"3d");
          Defvar(text(pt)+"3d",pt3);
          flg=2;
        );
      );
      if(flg==0,
        tmp=select(VLIST,#_1==text(pt)+"3d");
        if(length(tmp)>0, //17.10.07
          pt3=tmp_1_2;
          pt.xy=Parapt(pt3);
          tmp=ptz+".x=pt.x+NE.x-SW.x";
          parse(tmp);
        ); //17.10.07
      );
      if(isselected(pt) % isselected(parse(ptz)),
        drawtext(pos,text(pt3));
      );
      if(!contains(["p","q"],text(pt)),
        ptL=append(ptL,pt);
      );
    ,
      tmp=Findgeoinfo(pt);
      tmp1=[parse(tmp_1),parse(tmp_2),parse(tmp_3)];
      plistrest=append(plistrest,pt);
      inforest=append(inforest,tmp);
    );
  );
  if(length(plistrest)>0,
    tmp=Sortpointlist([plistrest,inforest]);
    plist=tmp_1;
    ilist=tmp_2;
    forall(1..length(plist),
      pt=plist_#;
      ptz=text(pt)+"z";
      tmp=ilist_#;
      p1=parse(tmp_3);
      p2=p1+parse(tmp_2)-parse(tmp_1);
      tmp1=p2-p1;
      tmp2=pt-p1;
      if(abs(tmp1_1)>abs(tmp1_2),
        par=tmp2_1/tmp1_1;
      ,
        par=tmp2_2/tmp1_2;
      );
      tmp=ilist_#;
      tmp1=parse(tmp_1+"3d");
      tmp2=parse(tmp_2+"3d");
      tmp3=parse(tmp_3+"3d");
      tmp1=(1-par)*tmp3+par*(tmp3+tmp2-tmp1);
      Putpoint(ptz,[pt.x+NE.x-SW.x,tmp1_3]);
      inspect(parse(ptz),"ptsize",3);
      inspect(parse(ptz),"color",3);
      inspect(parse(ptz),"textsize",TSIZEZ);
      pt3=Xyzcoord(pt.x,pt.y,parse(text(ptz)+".y"));
      VLIST=select(VLIST,#_1!=tmp1+"3d");
      Defvar(text(pt)+"3d",pt3);
      vlistpre=VLIST; //16.08.20
      if(isselected(pt) % isselected(parse(ptz)),
        drawtext(pos,text(pt3));
      );
      if(!contains(["p","q"],text(pt)),
        ptL=append(ptL,pt);
      );
    );
  );
  ptL;
);

Mkseg3d():=Mkseg3d([]);
Mkseg3d(options):=(
  regional(seg,nmseg,def,ptA,ptB,segL,opstr,opcindy,
       name,strg,Ltype,Noflg,tht,tmp,tmp1,tmp2,tmp3);
  name="geoseg3d";
  strg=name+"=list(";
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  opcindy=tmp_(length(tmp));
  segL=[];
  forall(allsegments(),seg,
	inspect(seg,"labeled",false);
    tmp=Finddef(seg);
    def=tmp_1;
    ptA=tmp_2;
    ptB=tmp_3;
    if(ispoint(parse(ptA)) & ispoint(parse(ptB)),
      tmp1=select(VLIST,indexof(#_1,ptA+"3d")>0);
      tmp2=select(VLIST,indexof(#_1,ptB+"3d")>0);
      if(length(tmp1)>0 & length(tmp2)>0,
        tmp1="["+ptA+"3d,"+ptB+"3d]";
        nmseg=ptA+ptB;
//        Spaceline(nmseg,parse(tmp1),append(options,"Msg=nodisp")); //15.06.15
        Spaceline("-"+nmseg+"3d",[ptA+"3d",ptB+"3d"], // 16.04.07
		      append(options,"Msg=nodisp")); //15.06.15
        segL=append(segL,nmseg+"3d");// 16.04.07
        strg=strg+nmseg+"3d,";// 16.04.07
      );
    );
  );
  println("generate totally "+name);
  if(length(segL)>0,  // 15.03.06
    strg=substring(strg,0,length(strg)-1)+")";
    if(Noflg<3,
      GLIST=append(GLIST,strg);
    );
    tmp1=apply(segL,Dq+#+Dq);  //15.03.02
    tmp=name+"="+text(tmp1);
    parse(tmp);
  );
  segL;
);

Ptseg3data():=Ptseg3data([]);
//help:Ptseg3data();
//help:Ptseg3data([A,B]);
Ptseg3data(options):=(
  regional(pt,pt3,plist,tmp,tmp1,tmp2);
  if(isselected(TH) % isselected(FI),
    tmp=remove(allpoints(),[NE,SW,TH,FI]);
    tmp=remove(tmp,options); //16.11.12
    tmp=apply(tmp,text(#));
    plist=select(tmp,substring(#,length(#)-1,length(#))!="z");
    forall(plist,pt,
      tmp1=text(pt);
      tmp=select(VLIST,#_1==tmp1+"3d"); //17.10.07
      if(length(tmp)>0,
        pt3=tmp_1_2;
        tmp=tmp1+".xy=Parapt("+pt3+")_[1,2];";
        parse(tmp);
        if(SUBSCR==1,
          tmp=tmp1+"z"+".x=NE.x-SW.x+"+tmp1+".x";
          parse(tmp);
        );
      );
    );
  ,
    Mkpointlist(options); // 16.12.18
  );
  forall(alllines(),
    #.color=[0.5,0.5,1];
  );
  SEG3dlist=Mkseg3d(options);
  SEG3dlist;
);

Putonseg3d(name,ptL):=Putonseg3d(name,ptL_1,ptL_2,[]);
Putonseg3d(name,Arg1,Arg2):=(
  if(islist(Arg1),
    Putonseg3d(name,Arg1_1,Arg1_2,Arg2);
  ,
    Putonseg3d(name,Arg1,Arg2,[]);
  );
);
Putonseg3d(name,pt1,pt2,options):=(
//help:Putonseg3d("C",[A,B]);
//help:Putonseg3d("C",A,B);
  regional(par,pn1,pn2,tmp,tmp1,tmp2,tmp3);
  par=0.5;
  tmp=divoptions(options);
  if(length(tmp_6)>0,
    par=tmp_6_1;
  );
  Putonseg(name,pt1,pt2,[par]);
  inspect(parse(name),"ptsize",3);
  inspect(parse(name),"color",2);
  inspect(parse(name),"textsize",TSIZEZ);
  Putpoint(name+"z",[parse(name+".x"),0]);
  inspect(parse(name+"z"),"ptsize",3);
  inspect(parse(name+"z"),"color",3);
  inspect(parse(name+"z"),"textsize",TSIZEZ);
  pn1=text(pt1);
  pn2=text(pt2);
  tmp1=replace("PTCz.y=PTAz.y+|PTC-PTA|/|PTB-PTA|*(PTBz.y-PTAz.y)","PTC",name);
  tmp1=replace(tmp1,"PTA",pn1);
  tmp1=replace(tmp1,"PTB",pn2);
  tmp2=replace("PTCz.x=PTAz.x+|PTC-PTA|/|PTB-PTA|*(PTBz.x-PTAz.x)","PTC",name);
  tmp2=replace(tmp2,"PTA",pn1);
  tmp2=replace(tmp2,"PTB",pn2); 
  parse(tmp2);
  parse(tmp1);  
  tmp1=parse(name+".x");
  tmp2=parse(name+".y");
  tmp3=parse(name+"z.y"); 
  tmp=Xyzcoord(tmp1,tmp2,tmp3);
  Defvar(name+"3d",tmp);
);

Putpoint3d(ptslist):=Putpoint3d(ptslist,"",0);
Putpoint3d(Arg1,Arg2):=( // 16.03.02 from
  if(islist(Arg1),
    Putpoint3d(Arg1,Arg2,0);
  ,
    Putpoint3d(Arg1,Arg2,"fix");
  );
);
Putpoint3d(Arg1,Arg2,Arg3):=(
//help:Putpoint3d(["A",[2,1,3]]);
//help:Putpoint3d(["A",[2,1,3]],"fix");
//help:Putpoint3d("A",[2,1,3]);
//help:Putpoint3d(["A",[2,1,3]],["fix"]);
  regional(nn,kk,pt,pt3,ptslist,fixstr,tmp,tmp1,tmp2,tmp3); //16.08.19
  if(isstring(Arg1),
    ptslist=[Arg1,Arg2];
    fixstr=Arg3;
  ,
    ptslist=Arg1;
    fixstr=Arg2;
  ); 
  if(islist(fixstr),fixstr=fixstr_1);// 16.03.02 upto
  nn=length(ptslist)/2;
  forall(1..nn,kk, // 16.08.19from
    tmp1=ptslist_(2*kk-1);
    pt3=ptslist_(2*kk); // 16.08.19upto
    VLIST=select(VLIST,#_1!=tmp1+"3d");
    tmp2=parse(tmp1);
    if(ispoint(tmp2), // 16.05.26from
      if(islist(fixstr),tmp=fixstr_1,tmp=fixstr);
      tmp=Toupper(tmp);
      if(tmp!="FIX", 
        tmp=Xyzcoord(tmp2.x,tmp2.y,parse(tmp1+"z").y);
      ,
        tmp=pt3;
      ); 
      Defvar(tmp1+"3d",tmp);
    ,
      Defvar(tmp1+"3d",pt3);
    );   // 16.05.26upto
    Defvar(tmp1+"fix",0);
    pt=Parapt(pt3);
    Putpoint(tmp1,pt,parse(tmp1+".xy"));
    inspect(parse(tmp1),"ptsize",3);
    inspect(parse(tmp1),"color",2);
    inspect(parse(tmp1),"textsize",TSIZE);
    if(SUBSCR==1,
      tmp2=tmp1+"z";
      tmp=[pt.x+NE.x-SW.x,pt3_3];
      tmp1="[NE.x-SW.x+"+tmp1+".x,"+tmp2+"y]";
      Putpoint(tmp2,tmp,parse(tmp1));
      inspect(parse(tmp2),"ptsize",3);
      inspect(parse(tmp2),"color",3);
      inspect(parse(tmp2),"textsize",TSIZEZ);
    );
  );
  if(islist(fixstr),tmp=fixstr_1,tmp=fixstr);
  if(tmp=="fix" % tmp=="Fix",
    Fixpoint3d(ptslist);
  );
);

Fixpoint3d(ptlist):=(
// help:Fixpoint3d(["O",[0,0,0],"X",[1,0,0]]);
  regional(name,pt3,pt2,tmp,tmp1,tmp2);
  forall(1..(length(ptlist)/2),
    name=ptlist_(2*#-1);
    Defvar(name+"fix",1);
    pt3=ptlist_(2*#);
    pt2=Parapt(pt3);
    tmp1=textformat(pt2_1,5);
    tmp2=textformat(pt2_2,5);
    tmp=name+".xy=["+tmp1+","+tmp2+"]";
    parse(tmp);
    tmp1=name+".x+NE.x-SW.x";
    tmp2=textformat(pt3_3,5);
    tmp=name+"z.xy=["+tmp1+","+tmp2+"];";
    parse(tmp);
  );
);

Perppt(name,ptstr,pLstr):=Putperp(name,ptstr,pLstr,"draw");
Perppt(name,ptstr,pLstr,option):=Putperp(name,ptstr,pLstr,option);
Putperp(name,ptstr,pLstr):=Putperp(name,ptstr,pLstr,"put");
Putperp(name,ptstr,pLstr,option):=(
//help:Perppt("N","O","A-B-C");
//help:Perppt("N","O","A-B-C","put");
//help:Perppt("N","O","A-B","none");
  regional(out,Eps,N3d,sgstr,pP,pA,pB,pC,tmp,tmp1,flgpL);
  Eps=10^(-4);
  pP=parse(ptstr+"3d");
  tmp=indexof(pLstr,"-");
  pA=parse(substring(pLstr,0,tmp-1)+"3d");
  tmp1=substring(pLstr,tmp,length(pLstr));
  tmp=indexof(tmp1,"-");
  if(tmp>0,
    pB=parse(substring(tmp1,0,tmp-1)+"3d");
    pC=parse(substring(tmp1,tmp,length(tmp1))+"3d");
    flgpL=1;
  ,
    pB=parse(tmp1+"3d");
    flgpL=0;
  );
  if(flgpL==1,
    tmp=Crossprod(pB-pA,pC-pA);
    if(abs(tmp)<Eps,
      err("not a plane");
    ,
      tmp=tmp/|tmp|;
      N3d=pP+tmp;
      Defvar("tmp3d",N3d); // temporary
      sgstr=ptstr+"-tmp";
      tmp=IntersectsgpL(name,sgstr,pLstr,"none");
      VLIST=select(VLIST,#_1!="tmp3d");
      out=tmp_1;
      Defvar(name+"3d",tmp_1);
    );
  ,
    if(|pB-pA|<Eps,
      err("on the line");
    ,
      tmp1=(pB-pA)/|pB-pA|;
      tmp=Dotprod(pP-pA,tmp1);
      out=pA+tmp*tmp1;
      Defvar(name+"3d",out);
    );
  );
  if(islist(option),tmp=option_1,tmp=option);
  tmp=substring(tmp,0,1);
  if(tmp=="P" % tmp=="p",
    Putpoint3d([name,out]);
    Fixpoint3d([name,out]);
  );
  if(tmp=="D" % tmp=="d",
    Drawpoint3d(out);
  );
  out;
);  

Perpplane(name,ptstr,nvec):=
    Perpplane(name,ptstr,nvec,"draw");
Perpplane(name,ptstr,nstr,option):=(
//help:Perpplane("A-B","P",[1,3,2]);
//help:Perpplane("A-B","P",[0,0,1,0]);
  regional(Eps,nvec,sgstr,pP,v1,v2,v3,
    th,ph,pA,pB,tmp,tmp1,tmp2);
  Eps=10^(-4);
  if(isstring(nstr),nvec=parse(nstr+"3d"),nvec=nstr);
  pP=parse(ptstr+"3d");
  v3=nvec/|nvec|;
  th=THETA; ph=PHI;
  tmp=findangle(v3);
  THETA=tmp_1; PHI=tmp_2;
  v1=Cancoordpara([1,0,0]);
  v2=Cancoordpara([0,1,0]);
  THETA=th; PHI=ph;
  tmp=indexof(pLstr,"-");
  pA=pP+v1;
  pB=pP+v2;
  if(islist(option),tmp=option_1,tmp=option);
  tmp=Toupper(substring(tmp,0,1)); // 16.06.19
  if(tmp=="P",
    tmp=indexof(name,"-");
    tmp1=substring(name,0,tmp-1);
    tmp2=substring(name,tmp,length(name));
    Putpoint3d([tmp1,pA,tmp2,pB],["fix"]);
  );
  if(tmp=="D",
    Drawpoint3d(pA);
    Drawpoint3d(pB);
  );
  [pA,pB];
);

Drawpoint3d(pt3):=(
//help:Drawpoint3d(pt3d);
  regional(ptL,tmp,tmp1,tmp2);
  if(MeasureDepth(pt3)==0,ptL=[pt3],ptL=pt3);
  forall(ptL,
    tmp=parapt(#);
    draw(tmp_(1..2));
    if(SUBSCR==1,
      draw([tmp_1+NE.x-SW.x, #_3]);
    );
  );
);

Putaxes3d(size):=(
//help:Putaxes3d(5);
//help:Putaxes3d([1,2,3]);
  regional(sL);
  if(islist(size),sL=size,sL=[size,size,size]);
  Putpoint3d(["O",[0,0,0]],"fix"); //17.06.02from
  Putpoint3d(["X",[sL_1,0,0]],"fix");
  Putpoint3d(["Y",[0,sL_2,0]],"fix");
  Putpoint3d(["Z",[0,0,sL_3]],"fix"); 
//  Fixpoint3d(["O",[0,0,0]]);
//  Fixpoint3d(["X",[sL_1,0,0],"Y",[0,sL_2,0],"Z",[0,0,sL_3]]);//17.06.02fupto
);

IntersectsgpL(name,sgstr,pLstr):=
   IntersectsgpL(name,sgstr,pLstr,"draw");
IntersectsgpL(name,sgstr,pLstr,option):=(
//help:IntersectsgpL("R","P-Q","A-B-C");
//help:IntersectsgpL("R","P-Q","A-B-C","put");
//// help:IntersectsgpL("","P-Q","A-B-C","none");
//help:IntersectsgpL("",[p1,p2],[p3,p4,p5],"draw");
  regional(opstr,out,pP,pQ,pA,pB,pC,pH,pK,pR,tseg,tt,ss,Eps,flg,
    nvec,tmp,tmp1,tmp2,tmp3,tmp4);
  if(islist(option),opstr=option_1,opstr=option);
  Eps=10^(-4);
  flg=0;
  if(!isstring(sgstr),  // 15.05.29
    pP=sgstr_1;
    pQ=sgstr_2;
  ,
    tmp=indexof(sgstr,"-");
    if(tmp>0,  // 15.05.28
      pP=parse(substring(sgstr,0,tmp-1)+"3d");
      pQ=parse(substring(sgstr,tmp,length(sgstr))+"3d");
    ,
      tmp1=parse(sgstr);
      pP=tmp1_1;
      pQ=tmp1_2;
    );
  );
  if(!isstring(pLstr),  // 16.02.02
    pA=pLstr_1;
    pB=pLstr_2;
    pC=pLstr_3;
  ,
    tmp=indexof(pLstr,"-");
    pA=parse(substring(pLstr,0,tmp-1)+"3d");
    tmp1=substring(pLstr,tmp,length(pLstr));
    tmp=indexof(tmp1,"-");
    pB=parse(substring(tmp1,0,tmp-1)+"3d");
    pC=parse(substring(tmp1,tmp,length(tmp1))+"3d");
  );
  nvec=Crossprod(pB-pA,pC-pA);
  if(abs(Dotprod(nvec,pQ-pP))>Eps,
    pH=(Reflect3point(pP,[pA,pB,pC])+pP)/2; 
    pK=(Reflect3point(pQ,[pA,pB,pC])+pQ)/2;
    tmp1=pP-pH;
    tmp2=tmp1+pK-pQ;
    tmp=select(tmp2,abs(#)==max(apply(tmp2,abs(#))));
    tmp=select(1..3,tmp2_#==tmp_1);
    tmp=tmp_1;
    tseg=tmp1_tmp/tmp2_tmp;
    pR=(1-tseg)*pH+tseg*pK;
    tmp=substring(opstr,0,1);
    if(Toupper(tmp)=="P",
      Putpoint3d([name,pR]);
      Fixpoint3d([name,pR]);
    );
    if(Toupper(tmp)=="D",
      Drawpoint3d(pR);
    );
    tmp=Dotprod(pB-pA,pC-pA);
    tmp1=Dotprod(pB-pA,pB-pA);
    tmp2=Dotprod(pC-pA,pC-pA);
    tmp3=Dotprod(pR-pA,pB-pA);
    tmp4=Dotprod(pR-pA,pC-pA);
    ss=-(tmp*tmp4-tmp3*tmp2)/(tmp1*tmp2-tmp^2);
    tt=(tmp1*tmp4-tmp*tmp3)/(tmp1*tmp2-tmp^2);
    out=[pR,tseg,ss,tt];
  ,
    if(Toupper(substring(opstr,0,1))!="N",
      println("   "+sgstr+","+pLstr+" are parallel");
    );
    out=[];
  );
  out;
);

Bezier3d(nm,ptctrlist):=Bezier3(nm,ptctrlist);
Bezier3d(nm,Ag1,Ag2):=Bezier3(nm,Ag1,Ag2);
Bezier3d(nm,ptlistorg,ctrlistorg,options):=
    Bezier3(nm,ptlistorg,ctrlistorg,options);
//help:Bezier3d("1",["A","B","C"],["D","E","F","G"]);
Bezier3(nm,ptctrlist):=Bezier3(nm,ptctrlist_1,ptctrlist_2,[]);
Bezier3(nm,Ag1,Ag2):=(
  if(MeasureDepth(Ag1)==0,
    Bezier3(nm,Ag1,Ag2,[]);
  ,
    if(MeasureDepth(Ag1)>1,
      Bezier3(nm,Ag1_1,Ag1_2,Ag2);
    ,
      if(isstring(Ag_1_1),
        Bezier3(nm,Ag1,Ag2,[]);
      ,
        Bezier3(nm,Ag1_1,Ag1_2,Ag2);
      );
    );
  );
);
Bezier3(nm,ptlistorg,ctrlistorg,options):=( //17.10.08 greatly changed
  regional(name,name3,name2,tmp,tmp1,tmp2,
      Ltype,Noflg,opstr,eqL,Num,ii,knt,ctr,out);
  name="bz"+nm;
  name3="bz3d"+nm;
  name2="bz2d"+nm;
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  eqL=tmp_5;
  opcindy=tmp_(length(tmp));
  Num=20;
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=Toupper(substring(#,0,1));
    tmp2=substring(#,tmp,length(#));
    if(tmp1=="N",
      Num=parse(tmp2);
    );
  );
  ptlist=ptlistorg;
  if(isstring(ptlistorg_1),ptlist=apply(ptlistorg,parse(#+"3d")));
  ctrlist=ctrlistorg;
  if(isstring(ctrlistorg_1),ctrlist=apply(ctrlistorg,parse(#+"3d")));
  out=[];
  forall(1..(length(ptlist)-1),ii,
    knt=[ptlist_ii,ptlist_(ii+1)];
    ctr=[ctrlist_(2*ii-1),ctrlist_(2*ii)];
    tmp="Bezierpt(T,ptlist,ctrlist)";//17.01.04
    tmp=Assign(tmp,"ptlist",knt);
    tmp=Assign(tmp,"ctrlist",ctr);
    Spacecurve(name+text(ii),tmp,"T=[0,1]",append(eqL,"nodisp"));
    GLIST=GLIST_(1..(length(GLIST)-2));
    out=append(out,name3+text(ii));
  );
  tmp1=[];
  forall(1..length(out),
    tmp=parse(out_#);
    if(#==1,tmp1=tmp,tmp1=concat(tmp1,tmp_(2..length(tmp))));
  );
  if(Noflg<3,
    println("generate Beziercurve "+name3);
    tmp=name3+"="+tmp1;
    parse(tmp);
    tmp1=Projpara(name3,["nodata"]);
    tmp=name2+"="+textformat(tmp1,5);
    parse(tmp);	
    ptlist=apply(ptlistorg,#+"3d");
    ptlist=RSform(text(ptlist),1);
    ctrlist=[];
    forall(1..(length(ctrlistorg)/2),
      tmp=[ctrlistorg_(2*#-1),ctrlistorg_(2*#)];
      tmp=apply(tmp,#+"3d");
      ctrlist=append(ctrlist,tmp);
    );
    ctrlist=RSform(text(ctrlist),0);
    tmp=name3+"=Bezier("+ptlist+","+ctrlist+","+Dq+"Num="+text(Num)+Dq+")";
    GLIST=append(GLIST,tmp);
    tmp=name2+"=Projpara("+name3+")";
    GLIST=append(GLIST,tmp);
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    if(SUBSCR==1, //  15.02.11
      Subgraph(name3,opcindy);
    );
  );
  out;
);

Putbezier3data(name,pt3Lorg):=Putbezier3data(name,pt3Lorg,[]);
Putbezier3data(name,pt3Lorg,options):=( // 17.10.08 greatly changed
  regional(pt3L,psize,Deg,nn,numstr,
     tmp,tmp1,tmp2,tmp3,pts,ctrpts);
  tmp=Divoptions(options);
  Deg=3;
  tmp1=tmp_5;
  forall(tmp1,
    if(substring(#,0,1)=="D",
      tmp=indexof(#,"=");
      Deg=parse(substring(#,tmp,length(#)));
    );
  );
  pt3L=pt3Lorg;
  if(isstring(pt3L_1),pt3L=apply(pt3L,parse(#+"3d")));
  ctrpts=[];
  forall(2..length(pt3L),nn,  // 16.08.19from
    numstr=text(nn-1);  // 16.08.19
    if(Deg==3,
      tmp1=name+numstr+"p";
      tmp2=(2*pt3L_(nn-1)+pt3L_(nn))/3;
	  VLIST=select(VLIST,#_1!=tmp1+"3d"); // 16.08.19from
      Putpoint3d([tmp1,tmp2]);
      ctrpts=append(ctrpts,tmp1);
      tmp1=name+numstr+"q";
      tmp3=(pt3L_(nn-1)+2*pt3L_nn)/3;
	  VLIST=select(VLIST,#_1!=tmp1+"3d");
      Putpoint3d([tmp1,tmp3]);
      ctrpts=append(ctrpts,tmp1);
    ,
      tmp1=name+numstr+"p";
      tmp2=(pt3L_(nn-1)+pt3L_nn)/2;
	  VLIST=select(VLIST,#_1!=tmp1+"3d"); // 16.08.19upto
      Putpoint3d([tmp1,tmp2]); 
      ctrpts=append(ctrpts,tmp1);
    );
  );
  ctrpts;
);

Mkbezierptcrv3d(ptdata):=Mkbezierptcrv3(ptdata);
Mkbezierptcrv3d(ptdata,options):=Mkbezierptcrv3(ptdata,options);
//help:Mkbezierptcrv3d(["A","B","C","D"]);
Mkbezierptcrv3(ptdata):=Mkbezierptcrv3(ptdata,[]);
Mkbezierptcrv3(ptdata,options):=(  //17.10.08 greatly changed
  regional(Eps,name,bz,pt,p1,p2,p13d,p23d,pt3,
    Out,tt,pos,tmp,tmp1,tmp2);
  Eps=10^(-4);
  pos=[NE.x+1,NE.y];
  Out=[];
  tmp=floor((BezierNumber3-1)/26);// 15.02.23 from
  if(tmp==0,tmp="",tmp=text(tmp));
  tmp2=mod(BezierNumber3,26);
  if(tmp2==0,tmp2=26);
  name=unicode(text(96+tmp2),base->10)+tmp;
  tmp2=Putbezier3data(name,ptdata,options);
  Bezier3(name,ptdata,tmp2,options);
  Out=append(Out,tmp2);
  BezierNumber3=BezierNumber3+1;
  Out;
);

///////////////////////////////////////////

Readobj(filename):=Readobj(filename,[]);
Readobj(filename,options):=(
//help:Readobj("file.obj",["size=-3"]);
  regional(eqL,size,vL,fnL,tmp,tmp1,tmp2);
  size=1;
  tmp=Divoptions(options);
  eqL=tmp_5;
  forall(eqL,
    if(Toupper(substring(#,0,1))=="S",
      tmp=indexof(#,"=");
      size=parse(substring(#,tmp,length(#)));
    );
  );
  tmp=load(filename);
  tmp=tokenize(tmp,"v");
  tmp1=tmp_(2..(length(tmp)-1));
  tmp2=tmp_(length(tmp));
  tmp2=tokenize(tmp2,"f");
  tmp1=append(tmp1,tmp2_1);
  tmp1=apply(tmp1,Removespace(#));
  tmp1=apply(tmp1,replace(#,"  "," "));
  tmp1=apply(tmp1,"["+replace(#," ",",")+"]");
  vL=apply(tmp1,size*parse(#));
  tmp2=tmp2_(2..(length(tmp2)));
  tmp2=apply(tmp2,Removespace(#));
  tmp2=apply(tmp2,replace(#,"  "," "));
  tmp2=apply(tmp2,"["+replace(#," ",",")+"]");
  fnL=apply(tmp2,parse(#));
  [vL,fnL];
);

/////////////////////////////////////////

Readobj(filename,options):=(
//help:Readobj("file.obj",["size=-3"]);
  regional(eqL,size,vL,fnL,dtL,numer,flg,flg2,
     tmp,tmp1,tmp2,tmp3,tmp4,tmp5);
  size=1;
  tmp=Divoptions(options);
  eqL=tmp_5;
  forall(eqL,
    if(Toupper(substring(#,0,1))=="S",
      tmp=indexof(#,"=");
      size=parse(substring(#,tmp,length(#)));
    );
  );
  numer="-0123456789";
  tmp=load(filename);
  dtL=tokenize(tmp,"v ");
  vL=[];
  fnL=[];
  flg=0;
  flg2=0;
  forall(dtL,dt,
    tmp1=Removespace(dt);
    if(indexof(numer,substring(tmp1,0,1))>0,
      tmp2="";
      tmp=indexof(tmp1,"f");
      if(tmp>0,
        tmp2=substring(tmp1,tmp-1,length(tmp1));
        tmp1=substring(tmp1,0,tmp-1);
        if(substring(tmp1,length(tmp1)-1,length(tmp1))=="#",
          tmp2="#"+tmp2;
          tmp1=substring(tmp1,0,length(tmp1)-1);
        );
//        tmp=indexof(tmp2,"#");
//        if(tmp>0,
//          tmp2=substring(tmp2,0,tmp-1);
//        );
      );
      if(flg==1,
        tmp1="";
        flg=0;
      );
      if(length(tmp1)>0,
        tmp=indexof(tmp1,"#");
        if(tmp>0,
          tmp1=substring(tmp1,0,tmp-1);
          flg=1;
        );
        tmp=indexof(tmp1,"vt");
        if(tmp>0,
          tmp1=substring(tmp1,0,tmp-1);
        );
        tmp1=replace(tmp1,"  ",",");
        tmp1=replace(tmp1," ",",");
        tmp1="["+tmp1+"]";
        tmp1=size*parse(tmp1);
        vL=append(vL,tmp1);
      );
      if(length(tmp2)>0,
        tmp2=tokenize(tmp2,"f");
        if(length(tmp2_1)==0,
          tmp2=tmp2_(2..length(tmp2));
 //       ,
 //         if(tmp2_1=="#",
//            tmp2=tmp2_(2..length(tmp2));
//            tmp2_1="#"+tmp2_1;
//          );
        );
        forall(1..length(tmp2),tmp3,
          tmp=tmp2_tmp3;
          if(substring(tmp,length(tmp)-1,length(tmp))=="#",
            tmp2_(tmp3+1)="#"+Removespace(tmp2_(tmp3+1));
            tmp2_tmp3=substring(tmp,0,length(tmp)-1);
          );
          tmp5="";
          if(length(tmp2_tmp3)>0,
            tmp4=tokenize(tmp2_tmp3," ");
            forall(tmp4,
              if(length(text(#))>0,
                if(indexof(text(#),"/")==0,
                  tmp=text(#);
                ,
                  tmp=tokenize(text(#),"/");
                  tmp=tmp_1;
                );
                if(length(tmp)>0,
                  tmp5=tmp5+tmp+",";
                );
              );
            );
            tmp5=substring(tmp5,0,length(tmp5)-1);
            if(length(tmp5)>0,
              if(substring(tmp5,0,1)!="#",
                tmp5="["+tmp5+"]";
                fnL=append(fnL,parse(tmp5));
              );
            );
          );
        );
      );
    );
  );
  [vL,fnL];
);

Concatobj(objL):=Concatobj(objL,[]);
Concatobj(objL,options):=(
//help:Concatobj([polyhed,triangle]);
//help:Concatobj([polyhed,triangle],["Rmf=no"]);
//help:Concatobj([[A,B,C],[A,C,D]]);
  regional(obj,vL,fL,vadd,vtx,faces,fnew,face,jj,kk,vctr,eps,
    tmp,tmp1,tmp2,tmp3,eqL,rmf);
  tmp=divoptions(options);
  eqL=tmp_5;
  rmf="y"; // 16.08.19
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=Toupper(substring(#,0,1));
    tmp2=Toupper(substring(#,tmp,tmp+1));
    if(tmp1=="R",
      if(tmp2=="Y",rmf="y"); // 16.08.14
    );
  );
  eps=10^(-4);
  vL=[];
  fL=[];
  vctr=0;
  forall(objL,obj,
    if(isstring(obj),
      tmp1=parse(obj);
    ,
      tmp1=obj;
    );
    if(length(tmp1)>2,  // 16.02.28
      tmp=1..length(tmp1);
      tmp1=[tmp1,[tmp]];
    );
    vtx=tmp1_1;  // 16.02.11 from
    faces=tmp1_2;
    fnew=faces;
    vctr=0;
    vadd=[];
    forall(1..length(vtx),jj,
      tmp1=vtx_jj;
      tmp=select(1..length(vL),dist3d(vL_#,tmp1)<eps);
      if(length(tmp)==0,
        vadd=append(vadd,tmp1);
        vctr=vctr+1;
        tmp2=length(vL)+vctr;
      ,
        tmp2=tmp_1;
      );
      forall(1..length(faces),kk,
        face=faces_kk;
        tmp=select(1..length(face),face_#==jj);
        if(length(tmp)>0,
          tmp1=tmp_1;
          fnew_kk_tmp1=tmp2;
        );
      );
    );
    vL=concat(vL,vadd);
    fL=concat(fL,fnew);   // 16.02.11 upto
  );
  tmp2=apply(1..length(fL),1);
  if(rmf=="y",
    forall(1..length(fL),jj,
      tmp=fL_jj;
      tmp1=select((jj+1)..length(fL),fL_#==tmp);
      forall(tmp1,tmp2_#=0);
    );
    tmp=select(1..length(tmp2),tmp2_#==1);
    fL=fL_tmp;
  );
  [vL,fL];
);

if(1==0,

Mkobjfile(fname,objL):=Mkobjfile(Dirwork,fname,objL);
Mkobjfile(path,fnameorg,objL):=(
//help:Mkobj(filename,objlist);
  regional(fname,obj,vtx,face,tmp,tmp1,tmp2);
  fname=fnameorg;
  if(indexof(fname,".")==0,fname=fname+".obj");
  vtx=objL_1;
  face=objL_2;
  setdirectory(path);
  SCEOUTPUT = openfile(fname);
  forall(vtx,tmp1,
    tmp2=tmp1;
    if(ispoint(tmp1),
      tmp2=parse(text(tmp1)+"3d");
    );
    tmp="v "+format(tmp2_1,5)+" "+format(tmp2_2,5)+" "+format(tmp2_3,5);
    println(SCEOUTPUT,tmp);
  );
  forall(face,tmp1,
    tmp="f";
    forall(tmp1,
      tmp=tmp+" "+text(#);
    );
    println(SCEOUTPUT,tmp);
  );
  closefile(SCEOUTPUT);  
  setdirectory(Dirwork);
);

);

VertexandEdge(nm,vfnL):=VertexEdgeFace(nm,vfnL,[]);
VertexandEdge(nm,vfnL,options):=VertexEdgeFace(nm,vfnL,options);
VertexEdgeFace(nm,vfnL):=VertexEdgeFace(nm,vfnL,[]);  // 16.02.10
//help:VertexEdgeFace("1",[vL,fnL]);
//help:VertexEdgeFace("1",["A","B","C"]);
//help:VertexEdgeFace(options=["Pt=fix","Edg=geo"]);
VertexEdgeFace(nm,vfnLorg,optionorg):=(
  regional(name3,namev,namee,namef,vfnL,options,Noflg,eqL,strL,
      vL,eL,enL,face,edge,vtx,vname,fixflg,edgflg,tmp,tmp1,tmp2);
  name3="phvef"+nm;
  namev="phv3d"+nm;
  namee="phe3d"+nm;
  namef="phf3d"+nm;
  options=optionorg;
  vfnL=vfnLorg;  // 16.06.19from
  if(length(vfnL)>2,
    vfnL=[vfnL,[1..length(vfnL)]];
  );
  if(length(vfnL)==1,
    vfnL=[vfnL_1,[1..length(vfnL_1)]];
  );  // 16.06.19upto
  tmp=Divoptions(options);
  Noflg=tmp_2;
  eqL=tmp_5;
  strL=tmp_7;
  fixflg=1;
  edgflg=1;
  forall(eqL,
    tmp1=Toupper(substring(#,0,1));
    tmp=indexof(#,"=");
    tmp2=Toupper(substring(#,tmp,length(#)));
    if(tmp1=="P",
      if(tmp2=="FREE", fixflg=0);
      options=remove(options,[#]);
    );
    if(tmp1=="E",
      if(substring(tmp2,0,1)=="N", edgflg=0);
      options=remove(options,[#]);
    );
  );
  forall(strL,
    if(Toupper(#)=="FREE",
      fixflg=0;
      options=remove(options,[#]);
    );
  );
  vL=[];
  forall(1..length(vfnL_1),
    vtx=vfnL_1_#;  // 16.02.10 from
    if(isstring(vtx),vtx=parse(vtx)); // 16.06.19
    if(ispoint(vtx),
      tmp=parse(text(vtx)+"3d");
      vname=text(vtx);
//      tmp=parse(text(vtx)+"3d");
//      tmp="v"+text(#)+"3d="+textformat(tmp,5);
//      parse(tmp);
    ,
      vname="v"+text(#);
      if(fixflg==1,
        Putpoint3d(["v"+text(#),vtx],"fix"); 
      );
    );
    vL=append(vL,vname); // 16.02.10 upto
  );
  eL=[];
  forall(vfnL_2,face,
    forall(1..length(face),
      tmp1=face_#;
      if(#<length(face),
        tmp2=face_(#+1);
      ,
        tmp2=face_1;
      );
      if(tmp2<tmp1,
        tmp=tmp2; tmp2=tmp1; tmp1=tmp;
      );
//      tmp1="v"+text(tmp1)+"3d";
//      tmp2="v"+text(tmp2)+"3d";
      tmp1=vL_tmp1+"3d";
      tmp2=vL_tmp2+"3d";
      tmp=select(eL,#==[tmp1,tmp2] % #==[tmp2,tmp1]);
      if(length(tmp)==0,
        eL=append(eL,[tmp1,tmp2]);
      );
    );
  );
  if(Noflg<3,
    enL=[];
    forall(eL,edge,
      if(edgflg==0,
        if(Noflg<2,
          tmp1=edge_1;//parse(edge_1);
          tmp2=edge_2;//parse(edge_2);
          tmp="sl3d"+edge_1+edge_2;
          if(islist(parse(tmp)),
            Changestyle3d([tmp],["nodisp"]);
          );
          tmp="sl3d"+edge_2+edge_1;
          if(islist(parse(tmp)),
            Changestyle3d([tmp],["nodisp"]);
          );
          tmp=replace(edge_1+edge_2,"3d","");
          options=append(options,"Msg=no");
          Spaceline("-"+tmp,[tmp1,tmp2],options); //16.02.28
        );
        enL=append(enL,tmp+"3d"); //16.02.29
      ,
        tmp1=replace(edge_1,"3d","");
        tmp2=replace(edge_2,"3d","");
        tmp=tmp1+tmp2;
        create([tmp],"Segment",[parse(tmp1),parse(tmp2)]);
        enL=append(enL,tmp+"3d");//16.02.29
      );
    );
    if(Noflg<2,
      println("generate "+name3);
      println("   generate totally "+namee);
      println("   generate vertexes "+namev);
      println("   generate faces  "+namef);
    );
    tmp1="";
    forall(enL,
      tmp1=tmp1+Dq+#+Dq+",";
    );
    tmp1=substring(tmp1,0,length(tmp1)-1);
    tmp=namee+"=["+tmp1+"]";
    parse(tmp);
    tmp=namee+"=list("+tmp1+")";
    tmp=replace(tmp,Dq,"");
    GLIST=append(GLIST,tmp);
    tmp1=apply(vL,Dq+#+"3d"+Dq);
    tmp1=text(tmp1);
    tmp1=substring(tmp1,1,length(tmp1)-1);
    tmp=namev+"=["+tmp1+"]";
    parse(tmp);
    tmp1=replace(tmp1,Dq,"");
    tmp=namev+"=list("+tmp1+")";
    GLIST=append(GLIST,tmp);
    tmp1=text(vfnL_2);
    tmp1=substring(tmp1,1,length(tmp1)-1);
    tmp=namef+"=["+tmp1+"]";
    parse(tmp);
    tmp=namef+"=list("+tmp1+")";
    GLIST=append(GLIST,tmp);
  );
  [namev,namee,namef];
);

Phparadata(nm,nmvf):=(
  regional(tmp1,tmp2);
  tmp1=parse("phv3d"+nmvf);
  tmp2=parse("phf3d"+nmvf);
  Phparadata(nm,nmvf,[tmp1,tmp2]);
);
Phparadata(nm,nmvf,Arg1):=(
  regional(vfL,options,tmp1,tmp2);
  if(islist(Arg1) & !islist(Arg1_1),
    tmp1=parse("phv3d"+nmvf);
    tmp2=parse("phf3d"+nmvf);
    vfL=[tmp1,tmp2];
    options=Arg1;
  ,
    vfL=Arg1;
    options=[];
  );
  Phparadata(nm,nmvf,vfL,options);
);
Phparadata(nm,nmvf,vfL,options):=(
//help:Phparadata("1","1",["do"]);
  regional(name2,name3,nameh2,nameh3,Ltype,Noflg,eqL,Hidden,
      opstr,opcindy,Outflg,Inflg,pdata1,tmp,tmp1,tmp2);
  name2="php2d"+nm;
  name3="php3d"+nm;
  nameh2="phh2d"+nm;
  nameh3="phh3d"+nm;
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  Inflg=tmp_3;
  Outflg=tmp_4;
  if(Inflg==0 & Outflg==0, Inflg=1;Outflg=1); // 15.05.15
  eqL=tmp_5;
  opstr=tmp_(length(tmp)-1);
  opcindy=tmp_(length(tmp));
  Hidden="";
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=Toupper(substring(#,0,1));
    tmp2=substring(#,tmp,length(#));
    if(tmp1=="H",
      Hidden=tmp2;
    );
  );
  if(Noflg<3,
    if(Outflg>=1,
      println("Output Phparadata "+name3);
      if(isstring(vfL_1),  // 2015.05.27 from
        tmp1=vfL_1;
      ,
        tmp1="list(";
        forall(vfL_1,
          tmp1=tmp1+#+",";
        );
        tmp1=substring(tmp1,0,length(tmp1)-1)+")";
      );
      if(isstring(vfL_2),
        tmp2=vfL_2;
      ,
        tmp2="list(";
        forall(vfL_2,
          tmp2=tmp2+text(#)+",";
        );
        tmp2=substring(tmp2,0,length(tmp2)-1)+")";
      );   // 2015.05.27 upto
      tmp=name3+"=Phparadata("+tmp1+","+tmp2+opstr+")";
      GLIST=append(GLIST,tmp);
      tmp=name2+"=Projpara("+name3+")";
      GLIST=append(GLIST,tmp);
      tmp=nameh3+"=PhHiddenData()";
      GLIST=append(GLIST,tmp);
      tmp=nameh2+"=Projpara("+nameh3+")";
      GLIST=append(GLIST,tmp);
    );
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    if(Inflg==1,
//      tmp=apply(pltdata1,replace(#,"3d","2d"));  // 15.05.18 from
      Changestyle3d("phe3d"+nmvf,["nodisp"]);
      GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    );
  );
  if(Hidden!="",
    Drawlinetype(nameh2,Hidden);
    if(Inflg==1,
      tmp1=Toupper(substring(Hidden,0,2));
      if(tmp1=="DR",Ltype=0);
      if(tmp1=="DA",Ltype=1);
      if(tmp1=="DO",Ltype=3);
      if(tmp1=="ID",Ltype=1);
      GCLIST=append(GCLIST,[nameh2,Ltype,opcindy]);
   );
  );
);

Nohiddenseg(seg,rng,triang):=(
// help:Nohiddenseg("1",seg1,[0,1],["v1","v2","v3"]);
  regional(Eps,pA3,pB3,pC3,pA,pB,pC,sg1,sg2,ss1,ss2,parL,flgL,flg1,flg2,
     veB,veC,sgnoh,sghid,added,tristr,tmp,tmp1,tmp2,tmp3,tmp4,tmp5);
  Eps=10^(-4);
  sgnoh=[0,1];
  pA3=if(isstring(triang_1),parse(triang_1+"3d"),triang_1); //16.06.19
  pA=Parapt(pA3);
  pB3=if(isstring(triang_2),parse(triang_2+"3d"),triang_2); //16.06.19
  pB=Parapt(pB3);
  pC3=if(isstring(triang_3),parse(triang_3+"3d"),triang_3); //16.06.19
  pC=Parapt(pC3);
  sg1=seg_1+rng_1*(seg_2-seg_1);
  sg2=seg_1+rng_2*(seg_2-seg_1);
  ss1=Parapt(sg1);
  ss2=Parapt(sg2);
  tristr=replace(text(triang),",","-");
  tristr=substring(tristr,1,length(tristr)-1);
  if(|ss2-ss1|>Eps & abs(Crossprod(pB-pA,pC-pA))>Eps,
    tmp=IntersectcrvsPp([ss1,ss2],[pA,pB,pC,pA]);
    parL=apply(tmp,#_2-1);
    parL=sort(parL);
    if(length(parL)==0,
      parL=[0,1];
    ,
      if(parL_1>Eps,parL=prepend(0,parL));
      if(parL_(length(parL))<1-Eps,parL=append(parL,1));
    );
    tmp1=Crossprod(pB3-pA3,pC3-pA3);
    if(abs(Dotprod(tmp1,sg2-sg1))>Eps,
      tmp=IntersectsgpL("",[sg1,sg2],tristr,["nodisp"]);
      if(tmp_2>-Eps & tmp_2<1+Eps,
        if(tmp_3>-Eps & tmp_4>-Eps & tmp_3+tmp_4<1+Eps,
          tmp1=select(parL,abs(tmp_2-#)<Eps);
          if(length(tmp1)==0,
            parL=sort(append(parL,tmp_2));
          );
        );
      );
    );
    flgL=[];
    forall(parL,
      tmp1=sg1+#*(sg2-sg1);
      tmp=Projcoordpara(tmp1);
      tmp_3=tmp_3+1;
      tmp2=Cancoordpara(tmp);
      tmp=IntersectsgpL("",[tmp1,tmp2],tristr,["nodisp"]);
      tmp1=Projcoordpara(tmp1);
      tmp2=Projcoordpara(tmp_1);
      flg1=0;
      if(tmp1_3<tmp2_3-Eps,flg1=-1);
      if(tmp1_3>tmp2_3+Eps,flg1=1);
      flgL=append(flgL,flg1);
    );
    sgnoh=[];
    forall(2..length(flgL),
      added=[];
      flg1=flgL_(#-1);
      flg2=flgL_#;
      if(max([flg1,flg2])==1 % flg1+flg2==0,
        added=[parL_(#-1),parL_#];
      ,
		sghid=[parL_(#-1),parL_#];
        tmp1=ss1+sghid_1*(ss2-ss1);
        tmp2=ss1+sghid_2*(ss2-ss1);
        tmp=IntersectcrvsPp([tmp1,tmp2],[pA,pB,pC,pA]);
        if(length(tmp)<2,
          veB=pB-pA;
          veC=pC-pA;
          tmp3=tmp1-pA;
          tmp4=tmp2-pA;
          tmp5=Crossprod(veB,veC);
          tmp1=(tmp3_1*veC_2-tmp3_2*veC_1)/tmp5;
          tmp2=(-tmp3_1*veB_2+tmp3_2*veB_1)/tmp5;
          if(tmp1>-Eps & tmp2>-Eps & tmp1+tmp2<1+Eps,flg1=1,flg1=0);
          tmp1=(tmp4_1*veC_2-tmp4_2*veC_1)/tmp5;
          tmp2=(-tmp4_1*veB_2+tmp4_2*veB_1)/tmp5;
          if(tmp1>-Eps & tmp2>-Eps & tmp1+tmp2<1+Eps,flg2=1,flg2=0);
          if(flg1==1 & flg2==0,
            if(length(tmp)>0,
              tmp1=tmp_1_2-1;
              tmp1=sghid_1+(sghid_2-sghid_1)*tmp1;
            ,
              tmp1=sghid_1;
            );
            added=[tmp1,sghid_2];
//        added=[tmp1+Eps,sghid_2];  // <--- USUI  2016.02.27
          );
          if(flg1==0 & flg2==1,
            if(length(tmp)>0,
              tmp1=tmp_1_2-1;
              tmp1=sghid_1+(sghid_2-sghid_1)*tmp1;
            ,
              tmp1=sghid_2;
            );
            added=[sghid_1,tmp1];
          );
          if(flg1+flg2==0,
            added=sghid;
          );
        );
        if(length(tmp)==2,
          if(min([flg1,flg2])>=0,added=sghid,added=[]);
        );
      );
      if(length(added)>0,
        if(length(sgnoh)==0,
          sgnoh=added;
        ,
          if(sgnoh_(length(sgnoh))<added_1-Eps,
             sgnoh=concat(sgnoh,added);
          ,
             sgnoh=sgnoh_(1..(length(sgnoh)-1));
             sgnoh=concat(sgnoh,added_(2..length(added)));
          );
        );
      );
    );
  );
  sgnoh=apply(sgnoh,rng_1+#*(rng_2-rng_1));
  sgnoh;
);

Nohiddensegs(seg,range,faceno,vtxL):=(
  regional(Eps,rng,face,vtx,out,ra,rb,tmp,tmp1,tmp2);
  Eps=10^(-4);
  face=apply(faceno,vtxL_#); // 16.02.10
  rng=range;
  out=rng;
  forall(2..(length(face)-1),vtx,
    tmp1=[face_1,face_vtx,face_(vtx+1)];
    forall(1..(length(rng)/2),
      tmp=[rng_(2*#-1),rng_(2*#)];
      out=remove(out,tmp);
      tmp2=Nohiddenseg(seg,tmp,tmp1);
      if(length(tmp2)>0,
        out=remove(out,tmp);
        out=concat(out,tmp2);
      );
    );
    tmp=sort(out);
    out=[];
    forall(1..(length(tmp)/2),
      ra=tmp_(2*#-1);
      rb=tmp_(2*#);
      if(ra<rb-Eps,
        if(length(out)==0,
          out=[ra,rb];
        ,
          if(out_(length(out))<ra-Eps,
            out=concat(out,[ra,rb]);
          ,
            out_(length(out))=rb;
          );
        );
      );
    );
    rng=out;
  );
  rng;
);

Nohiddenbyfaces(nm,facestr):=
    Nohiddenbyfaces(nm,Datalist3d(),facestr,[],["do"]);
Nohiddenbyfaces(nm,Arg1,Arg2):=(
  regional(segstr,facestr,options);
  if(islist(Arg2),
    segstr=Datalist3d();
    facestr=Arg1;
    options=Arg2;
  ,
    segstr=Arg1;
    facestr=Arg2;
    options=[];
  );
  Nohiddenbyfaces(nm,segstr,facestr,options,["do"]);
);
Nohiddenbyfaces(nm,Arg1,Arg2,Arg3):=(
  regional(segstr,facestr,options,optionsh);
  if(islist(Arg2),
    segstr=Datalist3d();
    facestr=Arg1;
    options=Arg2;
    optionsh=Arg3;
 ,
    segstr=Arg1;
    facestr=Arg2;
    options=Arg3;
    optionsh=["do"];
  );
  Nohiddenbyfaces(nm,segstr,facestr,options,optionsh); // 16.02.21
);
Nohiddenbyfaces(nm,segstr,facestr,optionorg,optionsh):=(
//help:Nohiddenbyfaces("1","phf3d1");
//help:Nohiddenbyfaces("1","ax3d","phf3d1");
//help:Nohiddenbyfaces(options1=["dr"]);
//help:Nohiddenbyfaces(options2=["do"]);
  regional(Eps,namen,nameh,options,segL,faceL,vtxL,rng,seg,face,
     flg,ctr,tmp,tmp1,tmp2,Ltype,Noflg,eqL,opstr,opcindy,frnL,frhL);
  Eps=10^(-4);
  namen="frn"+nm;
  nameh="frh"+nm;
  if(isstring(segstr),
//    segL=parse(segstr);
    segL=[segstr];  // 16.08.19
  ,
    segL=segstr;
  );
  tmp2=[];  // 16.02.29 from
  forall(segL,seg,
    tmp=replace(seg,"3d","2d");
    tmp1=select(GCLIST,#_1==tmp);
    tmp1=tmp1_1;
    if(tmp1_2==0,
      tmp2=append(tmp2,seg);
    );
  );
  segL=tmp2;
  Changestyle3d(segL,["nodisp"]); // 16.02.29 upto
  if(islist(segL),
    if(isstring(segL_1),
      segL=apply(segL,parse(#));
    );
  );
  segL=Flattenlist(segL);
  flg=0;
  if(isstring(facestr),
    if(substring(facestr,0,3)=="phf", // 16.08.19from
      faceL=parse(facestr);
      tmp=replace(facestr,"phf","phv"); // 16.02.10 from
      tmp=parse(tmp);
      vtxL=apply(tmp,replace(#,"3d","")); // 16.02.10 upto
      flg=1;
    ,
      faceL=parse(facestr);
    );
  );
  if(flg==0,
    if(length(faceL)==2, // 16.08.19upto
      vtxL=facestr_1;
      faceL=facestr_2;
    ,
      vtxL=facestr;
      faceL=[1..length(vtxL)];
    );
  ); 
//  Hidden="";
  options=optionorg;
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  eqL=tmp_5;
  opstr=tmp_(length(tmp)-1);
  opcindy=tmp_(length(tmp));
//  forall(eqL,
//    tmp=indexof(#,"=");
//    tmp1=Toupper(substring(#,0,1));
//    tmp2=substring(#,tmp,length(#));
//    if(tmp1=="H",
//      Hidden=tmp2;
//    );
//  );

  ctr=1;
  frnL=[];
  frhL=[];
  forall(segL,seg,
    rng=[0,1];
    forall(faceL,face,
      rng=Nohiddensegs(seg,rng,face,vtxL);
    );
    forall(1..length(rng)/2,
      tmp=rng_(2*#-1);
      tmp1=seg_1+tmp*(seg_2-seg_1);
      tmp=rng_(2*#);
      tmp2=seg_1+tmp*(seg_2-seg_1);
//      tmp=remove(options,eqL);
      tmp=append(options,"Msg=nodisp");  // 15.06.22
      Spaceline("-"+namen+"n"+text(ctr),[tmp1,tmp2],tmp);
      frnL=append(frnL,namen+"n"+text(ctr)+"3d");
      ctr=ctr+1;
    );
    if(length(rng)==0,
      rng=[0,1];
    ,
      if(rng_1>0+Eps,
        rng=prepend(0,rng);
      ,
        rng=rng_(2..length(rng));
      );
      if(rng_(length(rng))<1-Eps,
        rng=append(rng,1);
      ,
        rng=rng_(1..(length(rng)-1));
      );
    );
    forall(1..length(rng)/2,
      tmp=rng_(2*#-1);
      tmp1=seg_1+tmp*(seg_2-seg_1);
      tmp=rng_(2*#);
      tmp2=seg_1+tmp*(seg_2-seg_1);
      if(length(optionsh)>0,tmp=optionsh,tmp=["do"]);
      tmp=append(tmp,"Msg=nodisp");  // 15.06.22
      Spaceline("-"+nameh+"n"+text(ctr),[tmp1,tmp2],tmp);
      frhL=append(frhL,nameh+"n"+text(ctr)+"3d");
      ctr=ctr+1;
    );
  );
  tmp1=apply(frnL,Dq+#+Dq);
  tmp="frn3d"+nm+"="+text(tmp1);
  parse(tmp);
  println("   generate totally "+"frn3d"+nm);
  tmp1=apply(frhL,Dq+#+Dq);
  tmp="frh3d"+nm+"="+text(tmp1);
  parse(tmp);
  println("   generate totally "+"frh3d"+nm);
);

Faceremovaldata(nm,vfdata,crvdata):=Faceremovaldata(nm,vfdata,crvdata,[]);
Faceremovaldata(nm,vfdata,crvdata,options):=(
  regional(name2,name3,nameh2,nameh3,Ltype,Noflg,eqL,Hidden,
      opstr,opcindy,Outflg,Inflg,tmp,tmp1,tmp2);
  name2="frc2d"+nm;
  name3="frc3d"+nm;
  nameh2="frch2d"+nm;
  nameh3="frch3d"+nm;
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  Inflg=tmp_3;
  Outflg=tmp_4;
  if(Inflg==0 & Outflg==0, Inflg=1;Outflg=1); // 15.05.15
  eqL=tmp_5;
  opstr=tmp_(length(tmp)-1);
  opcindy=tmp_(length(tmp));
  Hidden="";
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=Toupper(substring(#,0,1));
    tmp2=substring(#,tmp,length(#));
    if(tmp1=="H",
      Hidden=tmp2;
    );
  );
  if(Noflg<3,
    if(Outflg>=1,
      println("Output Faceremovaldata "+name3);
      if(MeasureDepth(vfdata)==1,
        tmp1=text(apply(vfdata,#_[1,3]));
     ,
        tmp1=text([vfdata_[1,3]]);
     );
      tmp1=replace(tmp1,"[","list(");
      tmp1=replace(tmp1,"]",")");
      tmp2=text(crvdata);
      tmp2=replace(tmp2,"[","list(");
      tmp2=replace(tmp2,"]",")");
//      tmp2="Flattenlist("+tmp2+")";
      tmp=name3+"=Faceremovaldata("+tmp1+","+tmp2+opstr+","+Dq+"para"+Dq+")";
      GLIST=append(GLIST,tmp);
      tmp=name2+"=Projpara("+name3+")";
      GLIST=append(GLIST,tmp);
      tmp=nameh3+"=PhHiddenData()";
      GLIST=append(GLIST,tmp);
      tmp=nameh2+"=Projpara("+nameh3+")";
      GLIST=append(GLIST,tmp);
    );
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    if(Inflg==1,
      if(MeasureDepth(vfdata)==1,
        tmp1=apply(vfdata,parse(#_2));
      ,
        tmp1=[parse(vfdata_2)];
      );
      forall(tmp1,
         Changestyle3d(#,["nodisp"]);
       );
      Changestyle3d(crvdata,["nodisp"]);
      GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    );
  );
  if(Hidden!="",
    Drawlinetype(nameh2,Hidden);
    if(Inflg==1,
      tmp1=Toupper(substring(Hidden,0,2));
      if(tmp1=="DR",Ltype=0);
      if(tmp1=="DA",Ltype=1);
      if(tmp1=="DO",Ltype=3);
      if(tmp1=="ID",Ltype=1);
      GCLIST=append(GCLIST,[nameh2,Ltype,opcindy]);
    );
  );
);

Fullformfunc(FdL):=(
  regional(Out,nn,ii,Jrg,flg,Urg,Vrg,kk,Uname,
     Zf,Xname,Xf,Yname,Yf,tmp,tmp1,tmp2);
  Out=[FdL_1];
  nn=length(FdL);
  flg=0;
  forall(1..nn,
    if(flg==0,
      tmp=FdL_#;
      if(substring(tmp,length(tmp)-1,length(tmp))=="]",
        Jrg=#;
        Urg=FdL_Jrg;
        flg=1;
      );
    );
  );
  kk=indexof(Urg,"=");
  Uname=Removespace(substring(Urg,0,kk-1));
  tmp=substring(Urg,kk,length(Urg));
  Urg=Uname+"="+tmp;
//  tmp1=parse(tmp);
//  Urg=Uname+"="+textformat(tmp1,5);
  Vrg=FdL_(Jrg+1);
  kk=indexof(Vrg,"=");
  Vname=Removespace(substring(Vrg,0,kk-1));
  tmp=substring(Vrg,kk,length(Vrg));
  Vrg=Vname+"="+tmp;
//  tmp1=parse(tmp);
//  Vrg=Vname+"="+textformat(tmp1,5);
  if(Jrg==2,
    tmp=FdL_1;
    kk=indexof(tmp,"=");
    Zf=substring(tmp,kk,length(tmp));
    tmp=[Uname,Vname,Zf,Urg,Vrg];
    Out=concat(Out,tmp);
  ,
    if(Jrg==4,
      tmp=FdL_1;
      kk=indexof(tmp,"=");
      Zf=substring(tmp,kk,length(tmp));
      tmp=FdL_2;
      kk=indexof(tmp,"=");
      Xname=Removespace(substring(tmp,0,kk-1));
      Xf=substring(tmp,kk,length(tmp));
      tmp=FdL_3;
      kk=indexof(tmp,"=");
      Yname=Removespace(substring(tmp,0,kk-1));
      Yf=substring(tmp,kk,length(tmp));
      tmp=replace(Zf,Xname,"("+Xf+")");
      Zf=replace(tmp,Yname,"("+Yf+")");
      Tmp=[Xf,Yf,Zf,Urg,Vrg];
      Out=concat(Out,Tmp);
    ,
      tmp=FdL_2;
      kk=indexof(tmp,"=");
      Xf=substring(tmp,kk,length(tmp));
      tmp=FdL_3;
      kk=indexof(tmp,"=");
      Yf=substring(tmp,kk,length(tmp));
      tmp=FdL_4;
      kk=indexof(tmp,"=");
      Zf=substring(tmp,kk,length(tmp));
      tmp=[Xf,Yf,Zf,Urg,Vrg];
      Out=concat(Out,tmp);
    );
  );
  DrwS="enws";
  BdyL=[];
  forall((Jrg+2)..length(FdL),ii,
    tmp=FdL_ii;
    if(isstring(tmp),
      if(length(tmp)==0,
        tmp=" ";
      );
      DrwS=tmp;
    );
    if(islist(tmp) & length(tmp_1)>1,
      BdyL=tmp;
    );
  );
  tmp=[DrwS,BdyL];
  Out=concat(Out,tmp);
);

Surffun(nm,Fd):=(
  regional(name,coord,var1,var2,rng1,rng2,bdy,tmp,tmp1,tmp2);
  tmp=Fullformfunc(Fd);
  rng1=tmp_5;
  rng2=tmp_6;
  bdy=tmp_7;
  coord="["+tmp_2+","+tmp_3+","+tmp_4+"]";
  tmp=indexof(rng1,"=");
  var1=substring(rng1,0,tmp-1);
  rng1=substring(rng1,tmp,length(rng1));
  tmp=indexof(rng2,"=");
  var2=substring(rng2,0,tmp-1);
  rng2=substring(rng2,tmp,length(rng2));
  name="Sfn"+nm+"("+var1+","+var2+")";
  Deffun(name,["regional(tmp)","tmp="+coord,"tmp"]);
  [name,var1,rng1,var2,rng2,bdy];
);

Sf3data(nm,fdata):=Sf3data(nm,fdata,[]);
Sf3data(nm,fdata,optionorg):=(
  regional(tmp,tmp1,tmp2);
  tmp=Surffun(nm,fdata);
  tmp1=tmp_2+"="+tmp_3;
  tmp2=tmp_4+"="+tmp_5;
  Sf3data(nm,tmp_1,tmp1,tmp2,optionorg);
);
Sf3data(nm,funstr,var1,var2):=Sf3data(nm,funstr,var1,var2,[]);
Sf3data(nm,funstr,var1org,var2org,optionorg):=(
//help:Sf3data("1",Fd);
//help:Sf3data(options=["Num=[25,25]","Wire=[20,20]"]);
  regional(name2,name3,var1,var2,Ltype,Noflg,opstr,opcindy,eqL,Num,
    Wire,varu,varv,rngu,rngv,sfdtuL,sfdtvL,options,
    tmp,tmp1,tmp2,tmp3);
  var1=replace(var1org,"%pi","pi");
  var2=replace(var2org,"%pi","pi");
  tmp=Divoptions(optionorg);
  Ltype=tmp_1;
  Noflg=tmp_2;
  eqL=tmp_5;
  opstr=tmp_(length(tmp)-1);
  opcindy=tmp_(length(tmp));
  options=optionorg;
  Num=[25,25];
  Wire=[20,20];
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=substring(#,0,tmp-1);
    tmp2=substring(#,tmp,length(#)); // 16.05.25
    tmp=Toupper(substring(tmp1,0,1));
    if(tmp=="N",
      tmp2=parse(tmp2);
      if(!islist(tmp2),tmp2=[tmp2,tmp2]);
      Num=tmp2;
      options=remove(options,[#]);
    );
    if(tmp=="W",
      tmp2=parse(tmp2);
      if(!islist(tmp2),tmp2=[tmp2,tmp2]);
      Wire=tmp2;
      options=remove(options,[#]);
    );
  );
  tmp=indexof(var1,"=");
  varu=substring(var1,0,tmp-1);
  rngu=parse(substring(var1,tmp,length(var1)));
  tmp=indexof(var2,"=");
  varv=substring(var2,0,tmp-1);
  rngv=parse(substring(var2,tmp,length(var2)));
  sfdtuL=[];
  forall(0..Wire_2,
    tmp=rngv_1+#/Wire_2*(rngv_2-rngv_1);
    tmp1=replace(funstr,varv,textformat(tmp,5));
    tmp="Num="+text(Num_2);
    tmp2=concat(options,["Msg=no",tmp]);
    Spacecurve(nm+"u"+#,tmp1,var1,tmp2);
    sfdtuL=append(sfdtuL,"sc3d"+nm+"u"+#);
  );
  sfdtvL=[];
  forall(0..Wire_1,
    tmp=rngu_1+#/Wire_1*(rngu_2-rngu_1);
    tmp1=replace(funstr,varu,textformat(tmp,5));
    tmp="Num="+text(Num_1);
    tmp2=concat(options,["Msg=no",tmp]);
    Spacecurve(nm+"v"+#,tmp1,var2,tmp2);
    sfdtvL=append(sfdtvL,"sc3d"+nm+"v"+#);
  );
  if(Noflg<3,
    println(" generate totally "+"sf3d"+nm);
    tmp1="";
    forall(sfdtuL,
      tmp1=tmp1+Dq+#+Dq+",";
    );
    tmp1=substring(tmp1,0,length(tmp1)-1);
    tmp2="";
    forall(sfdtvL,
      tmp2=tmp2+Dq+#+Dq+",";
    );
    tmp2=substring(tmp2,0,length(tmp2)-1);
    tmp="sf3d"+nm+"=[["+tmp1+"],["+tmp2+"]]";
    parse(tmp);
  );
  [sfdtuL,sfdtvL];
);

Sfbdparadata(nm,fd):=
   Sfbdparadata(nm,fd,[],["nodisp"]);
Sfbdparadata(nm,fd,options):=
   Sfbdparadata(nm,fd,options,["nodisp"]);
Sfbdparadata(nm,fdorg,optionorg,optionsh):=(
//help:Sfbdparadata("1",Fd);
//help:Sfbdparadata(options2=["Wait=30",limit1([0.05,1], limit2(0.2)]);
  regional(fd,options,name2,name3,name2h,name3h,waiting,
     eqL,reL,strL,fname,outreg,tmp,tmp1,tmp2,flg,wflg);
  name2="sfbd2d"+nm;
  name3="sfbd3d"+nm;
  name2h="sfbdh2d"+nm;
  name3h="sfbdh3d"+nm;
  fname=Fhead+"sfbd"+nm+".txt";
  fd=apply(fdorg,if(isstring(#),"'"+#+"'",#));
  options=optionorg;
  tmp=Divoptions(options);
  eqL=tmp_5;
  reL=tmp_6;
  strL=tmp_7;
  waiting=30;
  outreg=0;  // 16.08.20
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=Toupper(substring(#,0,1));
    tmp2=substring(#,tmp,length(#));
    if(tmp1=="W",
      waiting=parse(tmp2);
      options=remove(options,[#]);
    );
//    if(tmp1=="O",  // 16.08.20
//      tmp=Toupper(substring(tmp2,0,1));
//      if(tmp=="T" % tmp=="Y", outreg=1,outreg=0);
//      options=remove(options,[#]);
//    );
  );
  options=remove(options,reL);
  wflg=0;
  forall(strL,
    tmp=Toupper(substring(#,0,1));
    if(tmp=="M",
      wflg=1;
      options=remove(options,[#]);
    );
    if(tmp=="R",
      wflg=-1;
      options=remove(options,[#]);
    );
  );
  cmdL=MkprecommandS(5);  // 16.02.01
  tmp=[
    Dq+fname+Dq,
    Dq+name3+Dq,name3,
    Dq+name2+Dq,name2,
    Dq+name3h+Dq,name3h,
    Dq+name2h+Dq,name2h,
    Dq+"HIDDENDATA"+Dq,"HIDDENDATA",  // 16.08.20from
    Dq+"PARTITIONPT"+Dq,"PARTITIONPT", 
    Dq+"OTHERPARTITION"+Dq,"OTHERPARTITION"  // 16.08.20upto
  ];
  fd=apply(fd,replace(#,"_1","(1)")); // 16.05.25from
  fd=apply(fd,replace(#,"_2","(2)"));
  fd=apply(fd,replace(#,"_3","(3)"));  // 16.05.25upto
  cmdL=concat(cmdL,[
    name3+"=Sfbdparadata",concat([fd,"Error"],reL),
    name2+"=Projpara",[name3],
    name3h+"=BorderHiddenData()",[],
    name2h+"=Projpara",[name3h],
    "function Ot=Hiddend(),global HIDDENDATA,Ot=HIDDENDATA,endfunction)"
       ,[],  // 16.08.20from
    "function Ot=Otherp(),global OTHERPARTITION,Ot=OTHERPARTITION,endfunction)"
       ,[],
    "HIDDENDATA=Hiddend()",[],
    "PARTITIONPT=PartitionPt()",[],
    "OTHERPARTITION=Otherp()",[], 
    "for J=1:length(OTHERPARTITION)",[],
    "  Tmp1=OTHERPARTITION(J)",[],
    "  Tmp2=[]",[],
    "  for K=1:length(Tmp1)",[],
    "    Tmp2=[Tmp2;Tmp1(K),1000]",[],
    "  end",[],
    "  OTHERPARTITION(J)=Tmp2",[],
    "end",[],
    "WriteOutData",tmp
  ]);
  options=append(options,"Wait="+text(waiting));
  if(wflg==1,options=concat(options,["m"])); // 16.08.20
  if(wflg==-1,options=concat(options,["r"]));
  if(ErrFlag==0,
    CalcbyS("sfbd"+nm,cmdL,options);
  );
  if(ErrFlag==1,
    err("Sfbdparadata not completed");
  ,
    if(outreg==1,
      OutFileList=remove(OutFileList,[fname]);
      OutFileList=append(OutFileList,fname);
    );
    ReadOutData(fname);
    if(islist(parse(name2)),
      Extractdata(name2,options);
      if(length(optionsh)>0,tmp=optionsh,tmp=["nodisp"]);
      Extractdata(name2h,tmp);
    ,
      ErrFlag=1;
    );
  );
);

Wireparadata(nm,out,wr1,wr2,fd):=
  Wireparadata(nm,out,fd,wr1,wr2,[],["nodisp"]);
Wireparadata(nm,out,fd,wr1,wr2,options):=
   Wireparadata(nm,out,fd,wr1,wr2,options,["nodisp"]);
Wireparadata(nm,outstr,fdorg,wr1,wr2,optionorg,optionsh):=(
//help:Wireparadata("1","sfbd3d1",fd,5,5);
//help:Wireparadata(options2=["Wait=40"]);
  regional(out,fd,options,name2,name3,name2h,name3h,outreg,
     eqL,reL,strL,fname,outfname,waiting,tmp,tmp1,tmp2,flg,wflg);
  name2="wire2d"+nm;
  name3="wiref3d"+nm;
  name2h="wireh2d"+nm;
  name3h="wireh3d"+nm;
  fname=Fhead+"wire"+nm+".txt";
  tmp=replace(outstr,"3d","");
  outfname=Fhead+tmp+".txt"; // 16.08.20
  out=parse(outstr);
  if(!islist(out), ErrFlag=1);
  fd=apply(fdorg,if(isstring(#),"'"+#+"'",#));
  options=optionorg;
  tmp=Divoptions(options);
  eqL=tmp_5;
  reL=tmp_6;
  strL=tmp_7;
  waiting=40;
  outreg=0;
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=Toupper(substring(#,0,1));
    tmp2=substring(#,tmp,length(#));
    if(tmp1=="W",
      waiting=parse(tmp2);
      options=remove(options,[#]);
    );
//    if(tmp1=="O", // 16.08.20from
//      tmp=Toupper(substring(tmp2,0,1));
//      if(tmp=="T" % tmp=="Y", outreg=1,outreg=0);
//      options=remove(options,[#]);
//    );// 16.08.20upto
  );
  options=remove(options,reL);
  wflg=0;
  forall(strL,
    tmp=Toupper(substring(#,0,1));
    if(tmp=="M",
      wflg=1;
      options=remove(options,[#]);
    );
    if(tmp=="R",
      wflg=-1;
      options=remove(options,[#]);
    );
  );
  flg=0;
  tmp1=floor(5*1000/WaitUnit);
  repeat(tmp1,
    if(flg==0,
      tmp=load(Fhead+replace(outstr,"3d","")+".txt");
      if(length(tmp)>=4,
        tmp2=substring(tmp,length(tmp)-4,length(tmp));
        if(tmp2=="////",
          flg=1;
        );
      ,
        wait(WaitUnit);
      );
    );
  );
  if(flg==0,
    Err(outstr+" file not copleted");
    ErrFlag=1;
  );
  if(ErrFlag==0,
//    Writeoutdata(outfname,["Out",out]);
  );
  cmdL=MkprecommandS(5);  // 16.02.01
  tmp=[
    Dq+fname+Dq,
    Dq+name3+Dq,name3,
    Dq+name2+Dq,name2,
    Dq+name3h+Dq,name3h,
    Dq+name2h+Dq,name2h
  ];
  fd=apply(fd,if(isstring(#),replace(#,"_1","(1)"),#)); // 16.05.25 08.17from
  fd=apply(fd,if(isstring(#),replace(#,"_2","(2)"),#)); 
  fd=apply(fd,if(isstring(#),replace(#,"_3","(3)"),#)); // 16.05.25 08.17upto
  cmdL=concat(cmdL,[
    "disp('Running Wireparadata')",[],
    "tmp=ReadOutData",[Dq+outfname+Dq],
    "execstr(tmp)",[],
    "function SetHidd(Dt),global HIDDENDATA,HIDDENDATA=Dt,endfunction)",
        [], // 16.08.20from
    "function SetPart(Dt),global PARTITIONPT,PARTITIONPT=Dt,endfunction)",
        [], 
    "function SetOther(Dt),global OTHERPARTITION,OTHERPARTITION=Dt,endfunction)",
        [], 
    "if type(HIDDENDATA)~=15,HIDDENDATA=list(HIDDENDATA),end",[],
    "if type(PARTITIONPT)~=15,PARTITIONPT=list(PARTITIONPT),end",[],
    "if type(OTHERPARTITION)~=15,OTHERPARTITION=list(OTHERPARTITION),end",[],
    "for J=1:length(OTHERPARTITION)",[],
    "  Tmp=OTHERPARTITION(J)",[],
    "  OTHERPARTITION(J)=Tmp(:,1)",[],
    "end",[],
    "SetHidd(HIDDENDATA)",[],
    "SetPart(PARTITIONPT)",[],
    "SetOther(OTHERPARTITION)",[],
    "if type("+outstr+")~=15,"+outstr+"=list("+outstr+"),end",[],  // 16.08.20upto
    name3+"=Wireparadata",concat([outstr,fd,wr1,wr2,"Error"],reL),
    name2+"=Projpara",[name3],
    name3h+"=WireHiddenData()",[],
    name2h+"=Projpara",[name3h],
    "WriteOutData",tmp
  ]);
  options=append(options,"Wait="+text(waiting));
  if(wflg==1,options=concat(options,["m"]));
  if(wflg==-1,options=concat(options,["r"]));
  if(ErrFlag==0,
    CalcbyS("wire"+nm,cmdL,options);
  );
  if(ErrFlag==1,
    err("Wireparadata not completed");
  ,
    if(outreg==1,
//      OutFileList=remove(OutFileList,[fname]);
//      OutFileList=append(OutFileList,fname);
    );
    ReadOutData(fname);
    if(!islist(parse(name2)), ErrFlag=1);
    if(ErrFlag==0,
      Extractdata(name2,options);
      if(length(optionsh)>0,tmp=optionsh,tmp=["nodisp"]);
      Extractdata(name2h,tmp);
    );
  );
);

Crvsfparadata(nm,gd,out,fd):=
  Crvsfparadata(nm,gd,out,fd,[],["nodisp"]);
Crvsfparadata(nm,gd,out,fd,options):=
   Crvsfparadata(nm,gd,out,fd,options,["nodisp"]);
Crvsfparadata(nm,gdstr,outstr,fdorg,optionorg,optionsh):=(
//help:Crvsfparadata("1","ax3d","sfbd3d1",fd);
//help:Crvsfparadata(options2=["Wait=20"]);
  regional(gd,out,fd,options,name2,name3,name2h,name3h,outreg,
     eqL,reL,strL,fname,gdfname,outfname,waiting,tmp,tmp1,tmp2,flg,wflg);
  name2="crvsf2d"+nm;
  name3="crvsf3d"+nm;
  name2h="crvsfh2d"+nm;
  name3h="crvsfh3d"+nm;
  fname=Fhead+"crvsf"+nm+".txt";
  gdfname=Fhead+"crv"+nm+".txt";
  tmp=replace(outstr,"3d","");
  outfname=Fhead+tmp+".txt"; // 16.08.20
  gd=parse(gdstr);
  out=parse(outstr);
  if(!islist(out), ErrFlag=1);
  fd=apply(fdorg,if(isstring(#),"'"+#+"'",#));
  options=optionorg;
  tmp=Divoptions(options);
  eqL=tmp_5;
  reL=tmp_6;
  strL=tmp_7;
  waiting=20;
  outreg=1;
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=Toupper(substring(#,0,1));
    tmp2=substring(#,tmp,length(#));
    if(tmp1=="W",
      waiting=parse(tmp2);
      options=remove(options,[#]);
    );
//    if(tmp1=="O",
//      tmp=Toupper(substring(tmp2,0,1));
//      if(tmp=="T" % tmp=="Y", outreg=1,outreg=0);
//      options=remove(options,[#]);
//    );
  );
  options=remove(options,reL);
  wflg=0;
  forall(strL,
    tmp=Toupper(substring(#,0,1));
    if(tmp=="M",
      wflg=1;
      options=remove(options,[#]);
    );
    if(tmp=="R",
      wflg=-1;
      options=remove(options,[#]);
    );
  );
  flg=0;
  tmp1=floor(5*1000/WaitUnit);
  repeat(tmp1,
    if(flg==0,
      tmp=load(Fhead+replace(outstr,"3d","")+".txt");
      if(length(tmp)>=4,
        tmp2=substring(tmp,length(tmp)-4,length(tmp));
        if(tmp2=="////",
          flg=1;
        );
      ,
        wait(WaitUnit);
      );
    );
  );
  if(flg==0,
    Err(outstr+" file not copleted");
    ErrFlag=1;
  );
  if(wflg==0,
    tmp=load(fname);
    if(length(tmp)==0,wflg=1);
  );
  if(ErrFlag==0,
//    Writeoutdata(outfname,["Gc",gd,"Out",out]);
//    wait(WaitUnit);
  );
  cmdL=MkprecommandS(5); // 16.02.01
  tmp=[
    Dq+fname+Dq,
    Dq+name3+Dq,name3,
    Dq+name2+Dq,name2,
    Dq+name3h+Dq,name3h,
    Dq+name2h+Dq,name2h
  ];
  fd=apply(fd,if(isstring(#),replace(#,"_1","(1)"),#)); // 16.05.25 08.17from
  fd=apply(fd,if(isstring(#),replace(#,"_2","(2)"),#)); 
  fd=apply(fd,if(isstring(#),replace(#,"_3","(3)"),#)); // 16.05.25 08.17upto
  cmdL=concat(cmdL,[
    "disp('Running Crvsfparadata')",[],
    "tmp=ReadOutData",[Dq+outfname+Dq],
    "execstr(tmp)",[],
    "function SetHidd(Dt),global HIDDENDATA,HIDDENDATA=Dt,endfunction)",
        [], // 16.08.20from
    "function SetPart(Dt),global PARTITIONPT,PARTITIONPT=Dt,endfunction)",
        [], 
    "function SetOther(Dt),global OTHERPARTITION,OTHERPARTITION=Dt,endfunction)",
        [], 
    "if type(HIDDENDATA)~=15,HIDDENDATA=list(HIDDENDATA),end",[],
    "if type(PARTITIONPT)~=15,PARTITIONPT=list(PARTITIONPT),end",[],
    "if type(OTHERPARTITION)~=15,OTHERPARTITION=list(OTHERPARTITION),end",[],
    "for J=1:length(OTHERPARTITION)",[],
    "  Tmp=OTHERPARTITION(J)",[],
    "  OTHERPARTITION(J)=Tmp(:,1)",[],
    "end",[],
    "SetHidd(HIDDENDATA)",[],
    "SetPart(PARTITIONPT)",[], 
    "SetOther(OTHERPARTITION)",[], 
    "if type("+outstr+")~=15,"+outstr+"=list("+outstr+"),end",[],  // 16.08.20upto
    name3+"=Crvsfparadata",concat([gdstr,outstr,fd,"Error"],reL),
    name2+"=Projpara",[name3],
    name3h+"=CrvsfHiddenData()",[],
    name2h+"=Projpara",[name3h],
    "WriteOutData",tmp
  ]);
  options=append(options,"Wait="+text(waiting));
  if(wflg==1,options=concat(options,["m"]));
  if(wflg==-1,options=concat(options,["r"]));
  if(ErrFlag==0,
    CalcbyS("crvsf"+nm,cmdL,options);
  );
  if(ErrFlag==1,
    err("Crvsfparadata not completed");
  ,
    if(outreg==1,
      OutFileList=remove(OutFileList,[fname]);
      OutFileList=append(OutFileList,fname);
    );
    ReadOutData(fname);
    tmp=replace(gdstr,"3d","2d");
    Changestyle([tmp],["nodisp"]);
    Extractdata(name2,options);
    if(length(optionsh)>0,tmp=optionsh,tmp=["nodisp"]);
    Extractdata(name2h,tmp);
  );
);

Intersectcrvsf(nm,gd,fd):=
  Intersectcrvsf(nm,gd,fd,[]);
Intersectcrvsf(nm,gdstr,fdorg,optionorg):=(
//help:Intersectcrvsf("1",ax3d_1,fd);
//help:Intersectcrvsf(options=["Wait=10",50,0.05]);
  regional(name,gd,fd,options,reL,fname,gdfname,
     waiting,tmp,tmp1,tmp2,flg,wflg,pts);
  name="intpts"+nm;
  fname=Fhead+name+".txt";
  gdfname=Fhead+"crv"+nm+".txt";
  gd=parse(gdstr);
  fd=apply(fdorg,if(isstring(#),"'"+#+"'",#));
  options=optionorg;
  tmp=Divoptions(options);
  eqL=tmp_5;
  reL=tmp_6;
  strL=tmp_7;
  waiting=10;
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=Toupper(substring(#,0,1));
    tmp2=substring(#,tmp,length(#));
    if(tmp1=="W",
      waiting=parse(tmp2);
      options=remove(options,[#]);
    );
  );
  options=remove(options,reL);
  wflg=0;
  forall(strL,
    tmp=Toupper(substring(#,0,1));
    if(tmp=="M",
      wflg=1;
      options=remove(options,[#]);
    );
    if(tmp=="R",
      wflg=-1;
      options=remove(options,[#]);
    );
  );
  if(wflg>-1,
    Writeoutdata(gdfname,["Gd",gd]);
//    wait(WaitUnit);
  );
  fd=apply(fd,replace(#,"_1","(1)")); // 16.05.25from
  fd=apply(fd,replace(#,"_2","(2)"));
  fd=apply(fd,replace(#,"_3","(3)"));  // 16.05.25upto
  cmdL=[
    "disp('Finding Intersectcrvsf')",[],
    "Setangle",[THETA,PHI]*180/pi,
    "tmp=ReadOutData",[Dq+gdfname+Dq],
    "execstr(tmp)",[],
    "Intpts=Intersectcrvsf",concat(["Gd",fd,"Error"],reL),
    "tmp1='['",[],
    "for J=1:length(Intpts)",[],
    "  tmp2=Intpts(J)",[],
    "  for K=1:length(tmp2)",[],
    "    if K==1",[],
    "      tmp1=tmp1+'['",[],
    "    end",[],
    "    tmp1=tmp1+string(tmp2(K))",[],
    "    if K<length(tmp2)",[],
    "      tmp1=tmp1+','",[],
    "    else",[],
    "      tmp1=tmp1+']'",[],
    "    end",[],
    "  end",[],
    "  if J<length(Intpts)",[],
    "    tmp1=tmp1+','",[],
    "  else",[],
    "    tmp1=tmp1+']||||'",[], //16.08.09
    "  end",[],
    "end",[],
    "fid=mopen('"+fname+"','w')",[],
    "  mputl(tmp1,fid)",[],
    "mclose(fid)",[]
  ];
  options=append(options,"Wait="+text(waiting));
  if(wflg==1,options=concat(options,["m"]));
  if(wflg==-1,options=concat(options,["r"]));
  if(ErrFlag==0,
    CalcbyS(name,cmdL,options); // 16.08.14
  );
  if(ErrFlag==1,
    err("Intersectcrvsf not completed");
  ,
    println(name+" obtained");  //16.08.14from
    pts=load(fname);
    pts=replace(pts,"||||","");
    tmp=name+"="+pts;
    parse(tmp);
    parse(pts);  //16.08.14upto
  );
);

Skeletonparadata2(nm):=Skeletonparadata2(nm,[]);
Skeletonparadata2(nm,options):=(
  regional(tmp,tmp2,tmp3); 
  //  tmp=apply(GCLIST,#_1);
  //  tmp=select(tmp,indexo(#,"2d")>0);
  //  tmp2=select(tmp,indexof(#,"sub")==0);
  tmp3=Datalist3d();
  Changestyle3d(tmp3,["nodisp"]);  // 15.06.02, 07.22
  //  tmp3=apply(tmp2,replace(#,"2d","3d"));
  Skeletonparadata2(nm,"",tmp3,tmp3,options);
);
Skeletonparadata2(nm,Arg1,Arg2):=(
  regional(nme,pltdata1,pltdata2,options);
  if(isstring(Arg1) & islist(Arg2),
    nme=Arg1;
//    pltdata1=parse("phe3d"+nme);
    pltdata1="phe3d"+nme;
    pltdata2=pltdata1;
    options=Arg2;
  ,
    nme="";
    pltdata1=Arg1;
    pltdata2=Arg2;
    options=[];
  );
  Skeletonparadata2(nm,nme,pltdata1,pltdata2,options);
);
Skeletonparadata2(nm,Arg1,Arg2,Arg3):=(
  regional(nme,pltdata1,pltdata2,options);
  if(!isstring(Arg1),
    nme="";
    pltdata1=Arg1;
    pltdata2=Arg2;
    options=Arg3;
  ,
    if(isstring(Arg2) & !isstring(Arg3),
      nme="";
      pltdata1=Arg1;
      pltdata2=Arg2;
      options=Arg3;
    ,
      nme=Arg1;
      pltdata1=Arg2;
      pltdata2=Arg3;
      options=[];
    );
  );
  Skeletonparadata2(nm,nme,pltdata1,pltdata2,options); // 15.07.09
);
Skeletonparadata2(nm,nme,pltdata1,pltdata2,options):=(
  regional(name2,name3,count,Ltype,Noflg,PdL,tmp,tmp1,tmp2,
      opstr,opcindy,Str,Outflg,Inflg,realL,pdata1,pdata2,level);
  name2="sk2d"+nm;
  name3="sk3d"+nm;
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  Inflg=tmp_3;
  Outflg=tmp_4;
  if(Inflg==0 & Outflg==0, Inflg=1;Outflg=1); // 15.05.15
  realL=tmp_6;
  opstr=tmp_(length(tmp)-1);
  opcindy=tmp_(length(tmp));
  if(Noflg<3,
    if(Outflg>=1,
      println("Output   Skeletonparadata2 "+name3);
      if(isstring(pltdata1),  // 15.05.27 from
        tmp1=pltdata1;
      ,
        tmp1="";
        forall(pltdata1,
          tmp1=tmp1+#+",";
        );
        tmp1="list("+substring(tmp1,0,length(tmp1)-1)+")";
      );
	  if(isstring(pltdata2),
        tmp2=pltdata2;
      ,
        tmp2="";
        forall(pltdata2,
          tmp2=tmp2+#+",";
        );
        tmp2="list("+substring(tmp2,0,length(tmp2)-1)+")";
	);
      Str="Skeletonpara3data("+tmp1+","+tmp2+opstr+")"; // 15.05.27 upto
      GLIST=append(GLIST,name3+"="+Str);
      tmp=name2+"=Projpara("+name3+")";
      GLIST=append(GLIST,tmp);
    );
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    if(Inflg==1,
//      tmp=apply(pltdata1,replace(#,"3d","2d"));  // 15.05.18 from
      if(nme!="",   // 15.05.27
        Changestyle3d("phe3d"+nme,["nodisp"]);
      ,
        forall(pltdata1,  // 15.07.16
          tmp=replace(#,"3d","2d");
          if(contains(GCLIST,tmp),
            Changestyle3d(#,["nodisp"]);
          );
        );
      );
      GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    );
  ); 
);

Skeletonparadata(nm):=Skeletonparadata(nm,[]);
Skeletonparadata(nm,options):=(
  regional(tmp,tmp2,tmp3); 
  tmp3=Datalist3d();
  Skeletonparadata(nm,"",tmp3,tmp3,options);
//  Changestyle3d(tmp3,["nodisp"]);  // 15.06.02, 07.22
);
Skeletonparadata(nm,Arg1,Arg2):=(
//help:Skeletonparadata("1");
//help:Skeletonparadata("1",[pdata1,pdata2],[pdata3]);
//help:Skeletonparadata(options2=[width(1), "Wait=20]);
  regional(nme,pltdata1,pltdata2,options);
  if(isstring(Arg1) & islist(Arg2),
    nme=Arg1;
//    pltdata1=parse("phe3d"+nme);
    pltdata1="phe3d"+nme;
    pltdata2=pltdata1;
    options=Arg2;
  ,
    nme="";
    pltdata1=Arg1;
    pltdata2=Arg2;
    options=[];
  );
  Skeletonparadata(nm,nme,pltdata1,pltdata2,options);
);
Skeletonparadata(nm,Arg1,Arg2,Arg3):=(
  regional(nme,pltdata1,pltdata2,options);
  if(!isstring(Arg1),
    nme="";
    pltdata1=Arg1;
    pltdata2=Arg2;
    options=Arg3;
  ,
    if(isstring(Arg2) & !isstring(Arg3),
      nme="";
      pltdata1=Arg1;
      pltdata2=Arg2;
      options=Arg3;
    ,
      nme=Arg1;
      pltdata1=Arg2;
      pltdata2=Arg3;
      options=[];
    );
  );
  Skeletonparadata(nm,nme,pltdata1,pltdata2,options); // 15.07.09
);
Skeletonparadata(nm,nme,pltdata1org,pltdata2org,optionorg):=(
  regional(name2,name3,eqL,strL,options,fname,outreg,waiting,
    reL, pltdata1,pltdata2,wflg,opcindy,width,pdata,
    tmp,tmp1,tmp2);
  name2="sk2d"+nm;
  name3="sk3d"+nm;
  fname=Fhead+"sk"+nm+".txt";
  pltdata1=[];// 16.01.31
  forall(pltdata1org,tmp1,
    tmp=parse(tmp1);
    if(isstring(tmp_1), 
       pltdata1=concat(pltdata1,tmp);
    ,
       pltdata1=append(pltdata1,tmp1);
    );
  );
  pltdata2=[];// 16.01.31
  forall(pltdata2org,tmp1,
    tmp=parse(tmp1);
    if(isstring(tmp_1), 
       pltdata2=concat(pltdata2,tmp);
    ,
       pltdata2=append(pltdata2,tmp1);
    );
  );
  options=optionorg;
  tmp=Divoptions(options);
  eqL=tmp_5;
  reL=tmp_6; //16.02.28 
  strL=tmp_7;
  opcindy=tmp_9;
  waiting=20;
  outreg=0;
  forall(eqL,
    tmp=indexof(#,"=");
    tmp1=Toupper(substring(#,0,1));
    tmp2=substring(#,tmp,length(#));
    if(tmp1=="W",
      waiting=parse(tmp2);
      options=remove(options,[#]);
    );
    if(tmp1=="O",
      tmp=Toupper(substring(tmp2,0,1));
      if(tmp=="T" % tmp=="Y", outreg=1,outreg=0);
      options=remove(options,[#]);
    );
  );
  wflg=0;
  forall(strL,
    tmp=Toupper(substring(#,0,1));
    if(tmp=="M",
      wflg=1;
      options=remove(options,[#]);
    );
    if(tmp=="R",
      wflg=-1;
      options=remove(options,[#]);
    );
  );
  width=1;
  if(length(reL)>0, //16.02.28 
    width=reL_1;
  );
  forall(pltdata1,pdata,
    tmp=select(GLIST,indexof(#,pdata+"=")>0);
    if(tmp==0,
      Defvar(pdata,parse(pdata));
    );
  );
  forall(pltdata2,pdata,
    tmp=select(GLIST,indexof(#,pdata+"=")>0);
    if(tmp==0,
      tmp=select(VLIST,#_1==pdata);
      if(tmp==0,
        Defvar(pdata,parse(pdata));
      );
    );
  );
  cmdL=MkprecommandS(4); 
  tmp2=[
    Dq+fname+Dq,
    Dq+name3+Dq,name3,
    Dq+name2+Dq,name2
  ];
  cmdL=concat(cmdL,[
    "Setangle",[THETA,PHI]*180/pi,
    name3+"=Skeletonpara3data",[pltdata1,pltdata2,width,"Error"],
    name2+"=Projpara",[name3],
    "WriteOutData",tmp2
  ]);
  options=append(options,"Wait="+text(waiting));
  if(wflg==1,options=concat(options,["m"]));
  if(wflg==-1,options=concat(options,["r"]));
  if(ErrFlag==0,
    CalcbyS("sk"+nm,cmdL,options);
  );
  if(ErrFlag==1,
    err("Skeletonparadata not completed");
  ,
    if(outreg==1,
      OutFileList=remove(OutFileList,[fname]);
      OutFileList=append(OutFileList,fname);
    );
    ReadOutData(fname);
    if(islist(parse(name2)),
      Extractdata(name2,options);
//      if(isstring(pltdata1_1),   // 16.01.31
        tmp=apply(pltdata1,replace(#,"3d","2d"));
//      ,
//        tmp=replace(pltdata1org,"3d","2d");
//      );
      Changestyle(tmp,["nodisp"]);
//      Changestyle3d(pltdata1,["nodisp"]);
    ,
      ErrFlag=1;
    );
  );
);

Skeletondatacindy(nm):=Skeletondatacindy(nm,[]);
Skeletondatacindy(nm,options):=(
  regional(tmp); 
  tmp=Datalist3d();
  Skeletondatacindy(nm,tmp,tmp,options);
);
Skeletondatacindy(nm,pltdata1,pltdata2):=
     Skeletondatacindy(nm,pltdata1,pltdata2,[]);
Skeletondatacindy(nm,pltdata1org,pltdata2org,options):=(
//help:Skeletondatacindy("1");
//help:Skeletondatacindy("1",[pdata1,pdata2],[pdata3]);
//help:Skeletondatacindy(options2=[1(width)]);
  regional(Eps,Eps2,name2,name3,Ltype,Noflg,reL,opcindy,
     Out,ObjL,Plt3L,Rr,pltdata1,pltdata2,Plt2L,ObjL,ii,Data,
     Obj3,jj,Gd,PtD,size,tmp,tmp1,tmp2);
  name2="sk2d"+nm;
  name3="sk3d"+nm;
  pltdata1=[];// 16.01.31
  forall(pltdata1org,tmp1,
    tmp=parse(tmp1);
    pltdata1=append(pltdata1,tmp);
  );
  pltdata2=[];// 16.01.31
  forall(pltdata2org,tmp1,
    tmp=parse(tmp1);
    pltdata2=append(pltdata2,tmp);
  );
  tmp=Divoptions(options);
  Ltype=tmp_1;
  Noflg=tmp_2;
  reL=tmp_6; //16.02.28 
  opcindy=tmp_9;
  Rr=0.075*1000/2.54/MilliIn;
  size=1;
  Eps2=0.05;
  if(length(reL)>0, //16.02.28 
    size=reL_1;
    Rr=size*Rr;
    if(length(reL)>1,Eps2=reL_2);
  );
  Eps=10^(-4);
  ObjL=Flattenlist(pltdata1);
  Plt3L=Flattenlist(pltdata2);
  tmp=apply(Plt3L,ProjcoordCurve(#));
  Plt2L=Flattenlist(tmp);
///////////  tmp=apply(pltdata1,replace(#,"3d","2d"));
  Out=[];
  forall(1..(length(ObjL)),ii,
    Obj3=ObjL_ii;
    tmp=ProjcoordCurve(Obj3);
    Data=Makeskeletondata([tmp],Plt2L,Rr,Eps2);
    forall(1..(length(Data)),jj,
      Gd=Data_jj;
      if((length(Gd)>1) 
       & (norm(Ptcrv(1,Gd)-Ptcrv(2,Gd))>Eps),
        PtD=[];
        forall(1..(length(Gd)),
          tmp=Gd_#;
          tmp1=Invparapt(tmp,Obj3);
          PtD=append(PtD,tmp1);
        );
        Out=append(Out,PtD);
      );
    );
  );
  Out=select(Out,length(Projcurve(#))>0); // 16.12.19
  tmp1=apply(Out,textformat(#,5));
  tmp=name3+"="+tmp1;
  parse(tmp);
  tmp=name2+"=Projcurve("+tmp1+");";
  parse(tmp);
  Changestyle3d(pltdata1org,["nodisp"]);
  if(Noflg<3,
    println("generate skeleton :"+name3);
    tmp1=text(pltdata1org);
    tmp1="list("+substring(tmp1,1,length(tmp1)-1)+")";
    tmp2=text(pltdata2org);
    tmp2="list("+substring(tmp2,1,length(tmp2)-1)+")";
    tmp=name3+"=Skeletonpara3data("+tmp1+","+tmp2+",";
    tmp=tmp+text(size)+")";
    GLIST=append(GLIST,tmp);  
    tmp=name2+"=Projpara("+name3+")";
    GLIST=append(GLIST,tmp);
  );
  if(Noflg<2,
    if(isstring(Ltype),
      Ltype=GetLinestyle(text(Noflg)+Ltype,name2);
    ,
      if(Noflg==1,Ltype=0);
    );
    GCLIST=append(GCLIST,[name2,Ltype,opcindy]);
    if(SUBSCR==1,
      Subgraph(name3,opcindy);
    );
  );
  Out;
);

Makeskeletondata(Obj2L,Plt2L,R0,Eps2):=(
  regional(Allres,Eps,Dmat,Dind,ii,Dt,nn1,nn2,Nind,Nobj,
      Plt2,PhL,ClipL,ns,pt1,pt2,pt,pta,ptb,za,zb,z1,z2,t1,t2,te,
	  koc,KukanL,nn,tt,Rr,Flg,ii,jj,ptq,hh,Ku,Res,contflg,breakflg);
  Eps=10.0^(-4);
  Dmat=[];
  Dind=[];
  forall(1..(length(Plt2L)),ii,
    Dt=Plt2L_ii;
    nn1=length(Dmat)+1;
    Dmat=concat(Dmat,Dt);
    nn2=length(Dmat);
    Dind=append(Dind,[nn1,nn2]);
  );
  Nind=length(Dind);
  Allres=[];
  forall(1..(length(Obj2L)),Nobj,
    Plt2=Obj2L_Nobj;
    PhL=Columnlist(Plt2,1..2);
	ClipL=[];
    forall(1..(length(PhL)-1),ns,
      pt1=PhL_(ns..(ns+1));
      forall(1..(length(Dind)),ii,
        tmp=Dind_ii;
        tmp=Dmat_(Dind_ii_1..Dind_ii_2);
        pt2=Columnlist(tmp,1..2);
        koc=IntersectcrvsPp(pt1,pt2,[Eps]);
        if(length(koc)>0,
          breakflg=0;
          forall(1..(length(koc)),jj,
            contflg=0;
            if(breakflg==0,
              pt=Op(1,Op(jj,koc));
              tmp=Op(2,Op(jj,koc));
              if((tmp<1+Eps) & (ns==1), 
                contflg=1;
              );    
              if(contflg==0,
                if((tmp>length(pt1)-Eps) 
                 & (ns==length(PhL)-1),
                  contflg=1;
                );
              );
              if(contflg==0,
                nn1=ns;
                nn2=Op(3,Op(jj,koc));
                tmp=Plt2_nn1;
                pta=tmp_(1..2);
                za=tmp_3;
                tmp=Plt2_(nn1+1);
                ptb=tmp_(1..2);
                zb=tmp_3;
                if(norm(pta-ptb)<Eps,
                  contflg=1;
                );
              );
              if(contflg==0,
                t1=norm(pta-pt)/norm(pta-ptb);
                z1=(1-t1)*za+t1*zb;
                tmp=Dmat_(Dind_ii_1..Dind_ii_2);
                tmp1=tmp_nn2;
                pta=tmp1_(1..2);
                za=tmp1_3;
                tmp2=tmp_(nn2+1);
                ptb=tmp2_(1..2);
                zb=tmp2_3;
                if(norm(pta-ptb)<Eps,
                  contflg=1;
                );
              );
              if(contflg==0,
                t2=norm(pta-pt)/norm(pta-ptb);
                z2=(1-t2)*za+t2*zb;
                if(z1<z2-Eps2,
                   if(length(ClipL)==0, 
                    tmp=1;
                  ,
                    tmp1=column(ClipL,1);
                    tmp1=apply(tmp1,#-pt_1);
                    tmp2=column(ClipL,2);
                    tmp2=apply(tmp2,#-pt_2);
                    tmp3=apply(tmp1,#^2)+apply(tmp2,#^2);
                    tmp=min(tmp3);         
                  );
                  if(tmp>Eps^2,
                    tmp1=pt1_2-pt1_1;
                    tmp2=ptb-pta;
                    tmp3=Dotprod(tmp1,tmp2);
                    tmp3=tmp3/norm(tmp1)/norm(tmp2);
                    tmp=1-0.5*tmp3^2;
                    tmp1=concat(pt,[nn1,t1,R0/tmp]);
                    ClipL=append(ClipL,tmp1);
                  );
                );
              );
            );
          );
        );
      );
    );
    te=length(Plt2);
    KukanL=[[1.0,te]];
    pt1=PhL;
    if(length(ClipL)>0,
      forall(1..(length(ClipL)),ii,
        tmp=ClipL_ii;
        pt=tmp_(1..2);
        nn=tmp_3;
        tt=nn+tmp_4;
        Rr=tmp_5;
        Flg=0;
        breakflg=0;
        forall(reverse(1..nn),jj,
          contflg=0;
          if(breakflg==0,
            ptq=PointonCurve(jj,pt1);
            if(norm(pt-ptq)<Rr,
              contflg=1;
            );
            if(contflg==0,
              Flg=jj;
              breakflg=1;
              contflg=1;
            );
          );
        );
        if(Flg==0,
          t1=1;
        ,
          t1=Flg; t2=tt;
          hh=t2-t1;
          forall(1..10,
            hh=hh*0.5;
            ptq=PointonCurve(t1+hh,pt1);
            if(norm(pt-ptq)<Rr,
              t2=t2-hh;
            ,
              t1=t1+hh;
            );
          );
        );
        Ku=[t1];
        Flg=0;
        breakflg=0;
        forall((nn+1)..te,jj,
          contflg=0;
          if(breakflg==0,
            ptq=PointonCurve(jj,pt1);
            if(norm(pt-ptq)<Rr,
              contflg=1;
            );
            if(contflg==0,
              Flg=jj;
              breakflg=1;
              contflg=1;
            );
          );
        );
        if(Flg==0,
          t2=te;
        ,
          t1=tt; t2=Flg;
          hh=t2-t1;
          forall(1..10,
            hh=hh*0.5;
            ptq=PointonCurve(t1+hh,pt1);
            if(norm(pt-ptq)<Rr,
              t1=t1+hh;
            ,
              t2=t2-hh;
            );
          );
        );
        Ku=append(Ku,t2);
        KukanL=Kukannozoku(Ku,KukanL);
	  );
    );
    Res=[];
    forall(1..(length(KukanL)),ii,
      tmp=KukanL_ii;
      t1=tmp_1; nn1=floor(t1);
      t2=tmp_2; nn2=floor(t2);
      PtL=[];
      if(t1-nn1<1-Eps,
        tmp=PointonCurve(t1,pt1);
        PtL=[tmp];
      );
      forall((nn1+1)..nn2,jj,
        tmp=PointonCurve(jj,pt1);
        PtL=append(PtL,tmp);
      );
      if(t2-nn2>Eps,
        tmp=PointonCurve(t2,pt1);
        PtL=append(PtL,tmp);
      );
      tmp=Listplot("",PtL,["nodata"]);
      Res=append(Res,tmp);
    );
    Allres=concat(Allres,Res);
  );
  Allres;
);

Kukannozoku(Jokyo,KukanL):=(
  regional(Res,Eps,nn,ii,t1,t2,Ku,Flg,contflg,tmp,tmp1);
  Eps=10^(-6);
  nn=length(KukanL);
  t1=Jokyo_1; t2=Jokyo_2;
  tmp=KukanL_1;
  t1=max(t1,tmp_1);
  tmp=KukanL_nn;
  t2=min(t2,tmp_2);
  Res=[];
  Flg=0;
  contflg=0;
  forall(1..nn,ii,
    if(contflg==0,
      Ku=KukanL_ii;
      if(Flg==0,
        if(Ku_2<t1,
          Res=append(Res,Ku);
        ,
          Flg=1;
          if(Ku_1<t1-Eps, 
            tmp=[Ku_1,t1];
            Res=append(Res,tmp);
          );
          if(Ku_2>t2+Eps,
            tmp=[t2,Ku_2];
            Res=append(Res,tmp);
          );
        );
      ,
        if(Flg==1,
          if(Ku_2<t2,
            contflg=1;
          ,
            if(contflg==0,
              Flg=2;
              if(Ku_1<t2-Eps,
                Ku=[t2,Ku_2];
              );
              Res=append(Res,Ku);
            );
          );
        ,
          if(contflg==0,
            Res=append(Res,Ku);
          );
        );
      );
    );
  );
  Res;
);

ProjcoordCurve(Curve):=(
  regional(SP,CP,ST,CT,Out,jj,pt,x,y,z,xz,yz,zz);
  SP=sin(PHI); CP=cos(PHI);
  ST=sin(THETA); CT=cos(THETA);
  Out=[];
  forall(1..(length(Curve)),jj,
    pt=Curve_jj;
    x=pt_1; y=pt_2; z=pt_3;
    xz=-x*SP+y*CP;
    yz=-x*CP*CT-y*SP*CT+z*ST;
    zz=x*CP*ST+y*SP*ST+z*CT;
    Out=append(Out,[xz,yz,zz]);
  );
  Out;
);

Divnohidhid(nm,dt3dorg,nvec):=Divnohidhid(nm,dt3dorg,nvec,[],["do"]);
Divnohidhid(nm,dt3dorg,nvec,optionorg,options2):=(
//help:Divnohidhid("1","sc3d1",nvec,["Num=100"],["Num=100","do"]);
  regional(name,dt3d,options,pvec,disp,nohid,hid,flg,tmp,tmp1,eqL);
  name="nh"+nm;
  options=optionorg;
  tmp=Divoptions(options);
  eqL=tmp_5;
  disp="Y";
  forall(eqL,
    tmp=Toupper(substring(#,0,1));
    if(tmp=="D",
      tmp=indexof(#,"=");
      disp=Toupper(substring(#,tmp,tmp+1));
      options=remove(options,[#]);
    );
  );
  if(isstring(dt3dorg),dt3d=parse(dt3dorg),dt3d=dt3dorg);
  pvec=[sin(THETA)*cos(PHI),sin(THETA)*sin(PHI),cos(THETA)];
  nohid=[];
  hid=[];
  flg=0;
  forall(1..(length(dt3d)-1),
    p1=dt3d_#;
    p2=dt3d_(#+1);
    tmp=assign(nvec,["x",p1_1,"y",p1_2,"z",p1_3]);
    tmp=parse(tmp);
    if(Dotprod(tmp,pvec)>0,
      if(flg==0 % flg==-1,
        nohid=append(nohid,[p1,p2]);
        flg=1;
      );
      if(flg==1,
        nohid_(length(nohid))=append(nohid_(length(nohid)),p2);
      );
    ,
      if(flg==0 % flg==1,
        hid=append(hid,[p1,p2]);
        flg=-1;
      );
      if(flg==-1,
        hid_(length(hid))=append(hid_(length(hid)),p2);
      );
    );      
  );
   if(disp=="Y",
    Changestyle3d([dt3dorg],["nodisp"]);
    forall(1..length(nohid),
      Spaceline(name+"nh"+text(#),nohid_#,options);
    );
    forall(1..length(hid),
      Spaceline(name+"h"+text(#),hid_#,options2);
    );
  );
  tmp1="[";
  forall(nohid,tmp1=tmp1+textformat(#,5)+",");
  tmp1=substring(tmp1,0,length(tmp1)-1)+"]";
  parse(name+"nh="+tmp1);
  tmp1="[";
  forall(hid,tmp1=tmp1+textformat(#,5)+",");
  tmp1=substring(tmp1,0,length(tmp1)-1)+"]";
  parse(name+"h="+tmp1);
  [nohid,hid];
);

Beziersurf(nm,m,n,pL):=(
//help:Beziersurf("pt",2,2,pL);
  regional(p3dL,ptlistx,ptlilsty,ptlistz);
  factorial(n):=(
    regional(out);
    out=1;
    forall(1..n,
      out=out*#;
    );
    out;
  );
  Deffun("Comb(n,r)",[
  "regional(y)",
  "y=factorial(n)/(factorial(r)*factorial(n-r))",
  "y"
  ]);
  Deffun("Bterm(u,v,m,n,i,j)",[
  "regional(y)",
  "y=Comb(m,i)*u^i*(1-u)^(m-i)",
  "y=y*Comb(n,j)*v^j*(1-v)^(n-j)",
  "y"
  ]);
  Deffun("Ball(u,v,m,n,pL)",[
  "regional(y,ii,jj,tmp,tmp1)",
  "y=0",
  "forall(0..m,ii,
    forall(0..n,jj,
      tmp=(n+1)*ii+jj+1;
      tmp1=Op(tmp,pL);
      tmp1=tmp1*Bterm(u,v,m,n,ii,jj);
      y=y+tmp1;
    )
  )",
  "y"
  ]);
  if(ispoint(pL_1),
    p3dL=apply(pL,parse(text(#)+"3d"));
  ,
    p3dL=apply(pL,parse(#+"3d"));
  );
  ptlistx=apply(p3dL,#_1);
  ptlisty=apply(p3dL,#_2);
  ptlistz=apply(p3dL,#_3);
  Defvar(nm+"x",ptlistx);
  Defvar(nm+"y",ptlisty);
  Defvar(nm+"z",ptlistz);
  tmp=[
  "p",
  "x=Ball(U,V,2,2,"+nm+"x)",
  "y=Ball(U,V,2,2,"+nm+"y)",
  "z=Ball(U,V,2,2,"+nm+"z)",
  "U=[0,1]",
  "V=[0,1]"
  ];
  tmp;
);

//help:end();
