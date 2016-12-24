INCLUDE Irvine32.inc
.data
;ROTORS, REFLECTOR AND INVERSES
sysmode    byte 3 dup(?),0
IntialSys  byte "<A> <A> <A>",0
plug       byte "ABCDEFGHIJKLMNOPQRSTUVWXYZ",0
alphasys   byte "ABCDEFGHIJKLMNOPQRSTUVWXYZ",0
RotorI     byte	"EKMFLGDQVZNTOWYHXUSPAIBRCJ",0
RotorII    byte	"AJDKSIRUXBLHWTMCQGZNPYFVOE",0
RotorIII   byte	"BDFHJLCPRTXVZNYEIWGAKMUSQO",0
Reflector  byte	"YRUHQSLDPXNGOKMIEBFZCWVJAT",0
plugspace  byte "||||||||||||||||||||||||||",0
nextpage   byte	"Press 'Enter' to go to the next page...",0
dis1       byte "                         This is The plugs system:--",0
dis2       byte "You can change this system, enter 1st character then the 2nd:",0
dis3       byte "                    ----------The plugs changed!----------",0
dis4       byte "                   This is The Intial System Mode for Rotors",0
dis5       byte "Enter your system: <",0
dis6       byte "Enter Text: ",0
dis7       byte "Encrypted Text: ",0
infuser    byte 1000 dup(?),0
infuserln  dword ?
x byte ?
y byte ?
a byte ?
b byte ?
countr dword 0
rot1 byte 0
rot2 byte 0
rot3 byte 0
;end


.code
main PROC
	call page1
	call clearscreen
	call page2
	call clearscreen
	call page3
	call encrypt
	mov edx,offset RotorI
	call writestring
	exit
main ENDP

page1 proc uses edx eax
	
	mov edx,offset dis1
	call writestring
	call crlf
	call crlf
	mov edx,offset alphasys
	call writestring
	call crlf
	mov edx,offset plugspace
	call writestring
	call crlf
	mov edx,offset alphasys
	call writestring
	call crlf
	call crlf
	mov edx,offset dis2
	call writestring
	call readchar
	mov x,al
	call writechar
	call readchar
	call writechar
	call readchar
	mov y,al
	call writechar
	call crlf
	mov ecx,26
	mov esi,offset plug
	swapchar:
		mov al,[esi]
		mov bl,x
		mov bh,y
		cmp al,bl
		je swapx
		jmp cont
		cont:
		cmp al,bh
		je swapy
		jmp en
		swapy:
		mov [esi],bl
		jmp en
		swapx:
		mov [esi],bh
		jmp en
		en:
		inc esi
	loop swapchar
	call crlf
	mov edx,offset dis3
	call writestring
	call crlf
	call crlf
	mov edx,offset alphasys
	call writestring
	call crlf
	mov edx,offset plugspace
	call writestring
	call crlf
	mov edx,offset plug
	call writestring
	call crlf
	call crlf
	ret
page1 endp

clearscreen proc uses edx eax
	mov edx,offset nextpage
	call writestring
	call readint
	call clrscr
	ret
clearscreen endp

page2 proc uses edx esi
	mov esi,offset sysmode
	mov edx,offset dis4
	call writestring
	call crlf
	call crlf
	mov edx,offset intialsys
	call writestring
	call crlf
	mov edx, offset dis5
	call writestring
	call readchar
	mov [esi],al
	call writechar
	inc esi
	mov al,62
	call writechar
	mov al,32
	call writechar
	mov al,60
	call writechar
	call readchar
	mov [esi],al
	call writechar
	inc esi
	mov al,62
	call writechar
	mov al,32
	call writechar
	mov al,60
	call writechar
	call readchar
	mov [esi],al
	call writechar
	mov al,62
	call writechar
	call crlf
	call crlf
	ret
page2 endp

page3 proc uses edx eax ecx
	mov edx, offset dis6
	call writestring
	mov edx,offset infuser
	mov ecx,lengthof infuser
	call readstring
	mov infuserln,eax
	call crlf
	mov edx,offset dis7
	call writestring
	call crlf
	call crlf
	ret
page3 endp

plugproc proc uses edx esi eax ecx edi
mov edi,offset plug
mov esi,offset alphasys
mov ecx,26
mov countr,0
alpha:
mov bl,[esi]
cmp bl,a
je eq1
inc countr
inc esi
loop alpha
eq1:
add edi,countr
mov ah,[edi]
mov a,ah
mov al,a
	ret
plugproc endp

roto1 proc uses esi ebx
mov esi,offset sysmode
mov bl,[esi]
sub bl,65
mov rot1,0
mov rot1,bl
movzx ecx,rot1
mov esi,offset RotorI
cmp rot1,0
je out1
r1:

innr1:

loop innr1

loop r1
out1:
	ret
roto1 endp

roto2 proc uses esi ebx
mov esi,offset sysmode
mov bl,[esi+1]
sub bl,65
mov rot2,0
mov rot2,bl
	ret
roto2 endp

roto3 proc uses esi ebx
mov esi,offset sysmode
mov bl,[esi+2]
sub bl,65
mov rot3,0
mov rot3,bl
	ret
roto3 endp

encrypt proc uses esi ebx 
mov esi,offset infuser
mov bl,[esi]
mov a,bl
call plugproc
call roto1
call roto2
call roto3
	ret
encrypt endp

END main
