// 08.07.13
// 08.10.16
// 09.03.05
// 09.05.12
// 10.05.09
// 14.07.05
// 16.12.24 ( when kukanL=[] )

function Allres=Makeskeletondata(Obj2L,Plt2L,R0,Eps2)
  Eps=10.0^(-3);
  Dmat=[];
  Dind=[];
  for I=1:Mixlength(Plt2L)
    Dt=Mixop(I,Plt2L);
    N1=size(Dmat,1)+1;
    Dmat=[Dmat;Dt];
    N2=size(Dmat,1);
    Dind=[Dind;N1,N2];
  end;
  Nind=size(Dind,1);
  Allres=[];
  for Nobj=1:Mixlength(Obj2L)
    Plt2=Mixop(Nobj,Obj2L);
    PhL=Plt2(:,1:2);
    ClipL=[];
    for Ns=1:size(PhL,1)-1
      P1=PhL(Ns:Ns+1,:);
      for I=1:size(Dind,1)
        Tmp=Dmat(Dind(I,1):Dind(I,2),:);
        P2=Tmp(:,1:2);       
        KC=IntersectcrvsPp(P1,P2,Eps);
        if Mixlength(KC)>0
          for J=1:Mixlength(KC)
            P=Mixop(1,Mixop(J,KC));
            Tmp=Mixop(2,Mixop(J,KC));
            if Tmp<1+Eps & Ns==1 // 2010.05.09       
              continue;
            end;    
            if Tmp>Numptcrv(P1)-Eps & Ns==size(PhL,1)-1            
              continue;
            end;               // 
            N1=Ns;
            N2=Mixop(3,Mixop(J,KC));
            Pa=Plt2(N1,1:2);
            Za=Plt2(N1,3);
            Pb=Plt2(N1+1,1:2);
            Zb=Plt2(N1+1,3);
            if norm(Pa-Pb)<Eps     // added from here
              continue;
            end;                   // upto here            
            T1=norm(Pa-P)/norm(Pa-Pb);
            Z1=(1-T1)*Za+T1*Zb;
            Tmp=Dmat(Dind(I,1):Dind(I,2),:);
            Pa=Tmp(N2,1:2);
            Za=Tmp(N2,3);
            Pb=Tmp(N2+1,1:2);
            Zb=Tmp(N2+1,3);
            if norm(Pa-Pb)<Eps     // added from here            
              continue;
            end;                   // upto here
            T2=norm(Pa-P)/norm(Pa-Pb);
            Z2=(1-T2)*Za+T2*Zb;
            if Z1<Z2-Eps2
              if ClipL==[]  // corrected from here
                Tmp=1;
              else
                Tmp1=ClipL(:,1)-P(1);
                Tmp2=ClipL(:,2)-P(2);
                Tmp3=Tmp1.^2+Tmp2.^2; // 20140705
                Tmp=min(Tmp3);              
              end;           // upto here
              if Tmp>Eps^2
                Tmp1=P1(2,:)-P1(1,:);
                Tmp2=Pb-Pa;
                Tmp3=Naiseki(Tmp1,Tmp2);
                Tmp3=Tmp3/norm(Tmp1)/norm(Tmp2);
                Tmp=1-0.5*Tmp3^2;
                ClipL=[ClipL;P,N1,T1,R0/Tmp];
              end;
            end;
          end;
        end;
      end;
    end;
    Te=size(Plt2,1);
    KukanL=[1.0,Te];
    P1=PhL;
    if size(ClipL,1)>0
      for I=1:size(ClipL,1)
        P=ClipL(I,1:2);
        NN=ClipL(I,3);
        T=NN+ClipL(I,4);
        R=ClipL(I,5);
        Flg=0;
        for J=NN:-1:1
          Q=PointonCurve(J,P1);
          if norm(P-Q)<R
            continue;
          end
          Flg=1;
          break;
        end;
        if Flg==0
          T1=1;
        else
          T1=J; T2=T;
          H=T2-T1;
          for N=1:10
            H=H*0.5;
            Q=PointonCurve(T1+H,P1);
            if norm(P-Q)<R
              T2=T2-H;
            else
              T1=T1+H;
            end;
          end;
        end;
        Ku=[T1];
        Flg=0;
        for J=NN+1:Te
          Q=PointonCurve(J,P1);
          if norm(P-Q)<R
            continue;
          end;
          Flg=1;
          break;
        end;
        if Flg==0
          T2=Te;
        else
          T1=T; T2=J;
          H=T2-T1;
          for N=1:10
            H=H*0.5;
            Q=PointonCurve(T1+H,P1);
            if norm(P-Q)<R
              T1=T1+H;
            else
              T2=T2-H;
            end;
          end;
        end;
        Ku=[Ku,T2];
        if size(KukanL,2)==0 then
          break;
        end;
        KukanL=Kukannozoku(Ku,KukanL);
      end;
    end;
    Res=[];
    for I=1:size(KukanL,1)
      T1=KukanL(I,1); N1=Trunc(T1);
      T2=KukanL(I,2); N2=Trunc(T2);
      PtL=[];
      if T1-N1<1-Eps
        Tmp=PointonCurve(T1,P1);
        PtL=MixS(Tmp);
      end;
      for J=N1+1:N2
        Tmp=PointonCurve(J,P1);
        PtL=Mixadd(PtL,Tmp);
      end;
      if T2-N2>Eps
        Tmp=PointonCurve(T2,P1);
        PtL=Mixadd(PtL,Tmp);
      end;
      Res=Mixadd(Res,Listplot(PtL));
    end;
    Allres=Mixjoin(Allres,Res);
  end;
endfunction;
