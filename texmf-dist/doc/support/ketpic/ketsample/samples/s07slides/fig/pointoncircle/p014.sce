//date time=2017/8/19 10:55:49

cd('/Users/takatoosetsuo/ketcindy/ketsample/samples/s07slides/fig');
Ketlib=lib('/Users/takatoosetsuo/ketcindy/ketlib/ketpicscifiles6');
Ketinit();
disp('KETpic '+ThisVersion())
Fnametex='pointoncircle/p014.tex';
Fnamesci='pointoncircle/p014.sce';
Fnamescibody='figs0706animatebody.sce';
Fnameout='figs0706animate.txt';
pi=%pi; i=%i;
arccos=acos; arcsin=asin; arctan=atan;

Setwindow([-2.5,2.5], [-2.5,2.5]);
Assignadd('pi',%pi);
Assignadd('XMIN',Xmin());
Assignadd('XMAX',Xmax());
Assignadd('YMIN',Ymin());
Assignadd('YMAX',Ymax());
A=[0,-4]; Assignrep('A',[0,-4]);
B=[6.28319,-3.96979]; Assignrep('B',[6.28319,-3.96979]);
C=[0.83543,-3.99598]; Assignrep('C',[0.83543,-3.99598]);
mdbw1=evstr('[-1.01718,0.07309]');  Assignrep('mdbw1',[-1.01718,0.07309]);
cr1=Circledata([[0,0],[2,0]]);
sg1=Listplot([[0,0],[-1.98423,-0.25067]]);
pt1=Pointdata([-1.98423,-0.25067]);
bw1=Bowdata([0,0],[-1.98423,-0.25067],1,0.3);
PtL=list(A,B,C);
GrL=list();
//if length(fileinfo(Fnamescibody))>0
//  Gbdy=ReadfromCindy(Fnamescibody);
//  execstr(Gbdy)
//end;

//Windisp(GrL,'c');

if 1==1 then

Openfile('/Users/takatoosetsuo/ketcindy/ketsample/samples/s07slides/fig/pointoncircle/p014.tex','1cm');
  Fontsize('s');
  Drwline(cr1);
  Letter([2,0],"se","$2$");
  Setcolor("red");
  Drwline(sg1);
  Setcolor("black");
  Setpen(0.12);
  Drwpt(list(pt1));
  Setpen(1);
  Letter([-1.02,0.07],"c","$2$");
  Drwline(bw1);
  Letter([-2.18,-0.28],"c","$\mathrm{P}$");
Closefile('1');

end;

quit();
