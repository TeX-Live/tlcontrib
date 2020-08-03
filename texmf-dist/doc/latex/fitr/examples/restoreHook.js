%
% Custom restore action
%
\begin{insDLJS}{rfitr}{Blink border on restore}
function pbRestoreHookCustom(event,bRestore) {
  console.show();
  app.beep();
  console.println("You just returned from a view window named \""
    +event.target.name+"\"!");
}
\end{insDLJS}
\endinput
