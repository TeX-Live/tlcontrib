function G=Gaiseki(a,b)
  if length(a)==3
    Tmp=[a(2)*b(3)-a(3)*b(2),...
         a(3)*b(1)-a(1)*b(3),...
         a(1)*b(2)-a(2)*b(1)];
  else
    Tmp=a(1)*b(2)-a(2)*b(1);
  end;
  G=Tmp;
endfunction

