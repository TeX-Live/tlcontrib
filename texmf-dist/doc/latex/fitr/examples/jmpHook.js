%
% Custom jump action
%
\begin{insDLJS}{jfitr}{Custom Effects following Jump}
function pbJmpHookCustom(event) {
  console.show();
  app.beep();
  console.println("You just jumped to a view window named \""
    +event.target.name+"\"!");
}
\end{insDLJS}
\endinput
