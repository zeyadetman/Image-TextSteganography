include irvine32.inc
INCLUDE macros.inc
.data
	text BYTE 125000 DUP(0),0
	buf Byte 5 DUP(0), 0
	handleP DWORD 0
	overP DWORD 0
	readP DWORD 0
.code

Decrypt PROC path: PTR BYTE
	invoke CreateFile, path,  3, 0, NULL, 3, 3, NULL
	mov handleP, eax
	invoke ReadFile, handleP,  offset buf, 5, offset readP, NULL
	mov ebx, 10
	mov esi, offset buf
	mov eax, 0
	mov ecx, 3
	getH:
		mul ebx
		mov dl, BYTE PTR [esi] 
		add eax, edx
		sub eax, '0'
		inc esi
	loop getH
	mov edx, eax
	push edx
	invoke ReadFile, handleP,  offset buf, 5, offset readP, NULL
	mov esi, offset buf
	mov eax, 0
	mov ecx, 3
	mov edx, 0
	getW:
		mul ebx
		mov dl, BYTE PTR [esi] 
		add eax, edx
		sub eax, '0'
		inc esi
	loop getW
	mov esi, offset text
	pop edx
	mul edx
	mov edx, 3
	mul edx
	mov ebx, 0
	mov edx, 0
	mov ecx, eax
	mov eax, 0
	iterate:
		pushad
		invoke ReadFile, handleP,  offset buf, 5, offset readP, NULL
		popad
		mov bl, BYTE PTR [buf+2]
		and ebx, 1
		shl eax, 1
		or eax, ebx
		push edx
		inc edx
		and edx, 7
		cmp edx, 0
		pop edx
		jne continue
		cmp al, 0
		je close
		mov [esi], al
		inc esi
		mov eax, 0
		continue:
		inc edx
	loop iterate
	close:
	invoke CloseHandle, handleP
	mov eax, offset text
	ret
Decrypt ENDP

Encrypt PROC path: PTR BYTE, msg: PTR BYTE
	invoke CreateFile, path,  3, 0, NULL, 3, 3, NULL
	mov handleP, eax
	invoke ReadFile, handleP,  offset buf, 5, offset readP, NULL
	mov ebx, 10
	mov esi, offset buf
	mov eax, 0
	mov ecx, 3
	mov edx, 0
	getH:
		mul ebx
		mov dl, BYTE PTR [esi] 
		add eax, edx
		sub eax, '0'
		inc esi
	loop getH
	mov edx, eax
	push edx
	invoke ReadFile, handleP,  offset buf, 5, offset readP, NULL
	mov esi, offset buf
	mov eax, 0
	mov ecx, 3
	mov edx, 0
	getW:
		mul ebx
		mov dl, BYTE PTR [esi] 
		add eax, edx
		sub eax, '0'
		inc esi
	loop getW
	mov esi, msg
	pop edx
	mul edx
	mov edx, 3
	mul edx
	mov ebx, 0
	mov edx, 0
	mov ecx, eax
	mov eax, 0
	iterate:
		cmp ecx, 0
		je close
		pushad
		invoke ReadFile, handleP,  offset buf, 5, offset readP, NULL
		popad
		pushad
		;inc edx
		mov esi, edx
		shr esi, 3
		add esi, msg
		and edx, 7
		mov ecx, edx
		movzx eax, BYTE PTR [esi]
		mov edx, 128
		shf:
			cmp ecx, 0
			je zero
			shr edx, 1
		loop shf
		zero:
		and eax, edx
		cmp eax, 0
		je donothing
		mov eax, 1
		donothing:
		movzx ebx, BYTE PTR [buf+2]
		and ebx, 1
		cmp eax, ebx
		je continue
		jl lose
		inc [buf+2]
		jmp continue
		lose:
		dec [buf+2]
		continue:
		mov dl, [buf+2]
		invoke SetFilePointer, handleP,  -5, NULL, 1
		invoke WriteFile, handleP,  offset buf, 5, offset readP, NULL
		popad
		inc edx
		dec ecx
	jmp iterate
	close:
	invoke CloseHandle, handleP
	ret
Encrypt ENDP


; DllMain is required for any DLL
DllMain PROC hInstance:DWORD, fdwReason:DWORD, lpReserved:DWORD
mov eax, 1 ; Return true to caller.
ret
DllMain ENDP
END DllMain