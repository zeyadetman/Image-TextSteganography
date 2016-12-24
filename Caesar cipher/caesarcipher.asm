INCLUDE Irvine32.inc
Str_ucase PROTO,
	pString:PTR BYTE
.data
inuser byte 1000 dup(?),0
n byte ?,0
T dword ?,0
countr dword 0
choice byte ?,0
str1 byte "Enter the text: ",0
str2 byte "Enter the key: ",0
str3 byte "Press '1' to encrypt, and '0' to decrypt : ",0
str4 byte "The Ciphered text is : ",0

.code
main PROC
	mov edx,offset str1
	call writestring
	mov edx,offset inuser
	mov ecx,lengthof inuser
	call readstring
	CALL TOUP
	CALL CRLF
	mov t,eax
	mov edx,offset str2
	call writestring
	call readint
	CALL CRLF
	add al,4
	mov n,al
	mov edx,offset str3
	call writestring
	call readint
	CALL CRLF
	mov choice,al
	cmp choice,1
	je enc
	call decrypt
	jmp outtt
	enc:
	call encrypt
	outtt:
	mov edx,offset str4
	call writestring
	mov edx,offset inuser
	call writestring
	CALL CRLF
	CALL CRLF
	exit
main ENDP

encrypt proc uses esi ecx ebx eax
	mov esi,offset inuser
	mov ecx,t
	l1:
		mov bl,[esi]
		cmp bl,65
		jae check1
		mov [esi],bl
		inc esi
		jmp l1
		check1:
		cmp bl,90
		jbe check3
		mov [esi],bl
		inc esi
		JMP L1
		check3:
		sub bl,65
		add bl,n
		cmp bl,25
		ja greater
		cmp bl,0
		jb lesser
		jmp out1
		greater:
		sub bl,26
		jmp out1

		lesser:
		add bl,26
		jmp out1

		out1:
		mov ah,0
		movzx ax,bl
		mov bl,26
		div bl
		add ah,65
		mov byte ptr[esi],ah
		mov al,ah
		;call writechar
		inc esi

	loop l1
	
	ret
encrypt endp

decrypt proc uses esi ecx ebx eax
mov esi,offset inuser
mov ecx,t
	l3:
		mov bl,[esi]
		cmp bl,65
		jae check
		mov [esi],bl
		inc esi
		jmp l3
		check:
		cmp bl,90
		jbe check2
		mov [esi],bl
		inc esi
		JMP L3
		check2:
		sub bl,65
		sub bl,n
		cmp bl,25
		ja greater
		cmp bl,0
		jb lesser
		jmp out1
		greater:
		ADD bl,26
		jmp out1

		lesser:
		SUB bl,26
		jmp out1

		out1:
		mov ah,0
		movzx ax,bl
		mov bl,26
		Idiv bl
		add ah,65
		mov byte ptr[esi],ah
		mov al,ah
		;call writechar
		inc esi

	loop l3

	ret
decrypt endp

TOUP PROC
	mov edx,OFFSET INUSER
	INVOKE Str_ucase,ADDR INUSER
    ;call writestring
    ;call crlf
    RET
TOUP ENDP

END main
