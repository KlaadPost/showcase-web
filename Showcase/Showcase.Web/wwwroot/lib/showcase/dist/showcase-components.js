"use strict";var at=Object.defineProperty;var it=(t,e,n)=>e in t?at(t,e,{enumerable:!0,configurable:!0,writable:!0,value:n}):t[e]=n;var Ce=(t,e,n)=>(it(t,typeof e!="symbol"?e+"":e,n),n);function b(){}function Ge(t){return t()}function We(){return Object.create(null)}function te(t){t.forEach(Ge)}function Xe(t){return typeof t=="function"}function B(t,e){return t!=t?e==e:t!==e||t&&typeof t=="object"||typeof t=="function"}function ot(t){return Object.keys(t).length===0}function u(t,e){t.appendChild(e)}function N(t,e,n){t.insertBefore(e,n||null)}function E(t){t.parentNode&&t.parentNode.removeChild(t)}function ut(t,e){for(let n=0;n<t.length;n+=1)t[n]&&t[n].d(e)}function g(t){return document.createElement(t)}function v(t){return document.createTextNode(t)}function S(){return v(" ")}function Ye(){return v("")}function A(t,e,n,l){return t.addEventListener(e,n,l),()=>t.removeEventListener(e,n,l)}function xe(t){return function(e){return e.preventDefault(),t.call(this,e)}}function p(t,e,n){n==null?t.removeAttribute(e):t.getAttribute(e)!==n&&t.setAttribute(e,n)}function ct(t){return Array.from(t.childNodes)}function X(t,e){e=""+e,t.data!==e&&(t.data=e)}function U(t,e){t.value=e??""}function Se(t,e,n,l){n==null?t.style.removeProperty(e):t.style.setProperty(e,n,l?"important":"")}let de;function me(t){de=t}function et(){if(!de)throw new Error("Function called outside component initialization");return de}function ft(t){et().$$.on_mount.push(t)}function pt(t){et().$$.after_update.push(t)}const oe=[],Le=[];let ue=[];const Ke=[],mt=Promise.resolve();let Pe=!1;function dt(){Pe||(Pe=!0,mt.then(tt))}function Ie(t){ue.push(t)}const Me=new Set;let ie=0;function tt(){if(ie!==0)return;const t=de;do{try{for(;ie<oe.length;){const e=oe[ie];ie++,me(e),ht(e.$$)}}catch(e){throw oe.length=0,ie=0,e}for(me(null),oe.length=0,ie=0;Le.length;)Le.pop()();for(let e=0;e<ue.length;e+=1){const n=ue[e];Me.has(n)||(Me.add(n),n())}ue.length=0}while(oe.length);for(;Ke.length;)Ke.pop()();Pe=!1,Me.clear(),me(t)}function ht(t){if(t.fragment!==null){t.update(),te(t.before_update);const e=t.dirty;t.dirty=[-1],t.fragment&&t.fragment.p(t.ctx,e),t.after_update.forEach(Ie)}}function gt(t){const e=[],n=[];ue.forEach(l=>t.indexOf(l)===-1?e.push(l):n.push(l)),n.forEach(l=>l()),ue=e}const ve=new Set;let le;function nt(){le={r:0,c:[],p:le}}function lt(){le.r||te(le.c),le=le.p}function P(t,e){t&&t.i&&(ve.delete(t),t.i(e))}function j(t,e,n,l){if(t&&t.o){if(ve.has(t))return;ve.add(t),le.c.push(()=>{ve.delete(t),l&&(n&&t.d(1),l())}),t.o(e)}else l&&l()}function ke(t){return(t==null?void 0:t.length)!==void 0?t:Array.from(t)}function _t(t,e){j(t,1,1,()=>{e.delete(t.key)})}function wt(t,e,n,l,i,r,a,s,o,c,y,_){let f=t.length,d=r.length,h=f;const K={};for(;h--;)K[t[h].key]=h;const J=[],H=new Map,Y=new Map,V=[];for(h=d;h--;){const $=_(i,r,h),w=n($);let T=a.get(w);T?l&&V.push(()=>T.p($,e)):(T=c(w,$),T.c()),H.set(w,J[h]=T),w in K&&Y.set(w,Math.abs(h-K[w]))}const ne=new Set,Q=new Set;function Z($){P($,1),$.m(s,y),a.set($.key,$),y=$.first,d--}for(;f&&d;){const $=J[d-1],w=t[f-1],T=$.key,G=w.key;$===w?(y=$.first,f--,d--):H.has(G)?!a.has(T)||ne.has(T)?Z($):Q.has(G)?f--:Y.get(T)>Y.get(G)?(Q.add(T),Z($)):(ne.add(G),f--):(o(w,a),f--)}for(;f--;){const $=t[f];H.has($.key)||o($,a)}for(;d;)Z(J[d-1]);return te(V),J}function z(t){t&&t.c()}function D(t,e,n){const{fragment:l,after_update:i}=t.$$;l&&l.m(e,n),Ie(()=>{const r=t.$$.on_mount.map(Ge).filter(Xe);t.$$.on_destroy?t.$$.on_destroy.push(...r):te(r),t.$$.on_mount=[]}),i.forEach(Ie)}function W(t,e){const n=t.$$;n.fragment!==null&&(gt(n.after_update),te(n.on_destroy),n.fragment&&n.fragment.d(e),n.on_destroy=n.fragment=null,n.ctx=[])}function bt(t,e){t.$$.dirty[0]===-1&&(oe.push(t),dt(),t.$$.dirty.fill(0)),t.$$.dirty[e/31|0]|=1<<e%31}function R(t,e,n,l,i,r,a=null,s=[-1]){const o=de;me(t);const c=t.$$={fragment:null,ctx:[],props:r,update:b,not_equal:i,bound:We(),on_mount:[],on_destroy:[],on_disconnect:[],before_update:[],after_update:[],context:new Map(e.context||(o?o.$$.context:[])),callbacks:We(),dirty:s,skip_bound:!1,root:e.target||o.$$.root};a&&a(c.root);let y=!1;if(c.ctx=n?n(t,e.props||{},(_,f,...d)=>{const h=d.length?d[0]:f;return c.ctx&&i(c.ctx[_],c.ctx[_]=h)&&(!c.skip_bound&&c.bound[_]&&c.bound[_](h),y&&bt(t,_)),f}):[],c.update(),y=!0,te(c.before_update),c.fragment=l?l(c.ctx):!1,e.target){if(e.hydrate){const _=ct(e.target);c.fragment&&c.fragment.l(_),_.forEach(E)}else c.fragment&&c.fragment.c();e.intro&&P(t.$$.fragment),D(t,e.target,e.anchor),tt()}me(o)}class O{constructor(){Ce(this,"$$");Ce(this,"$$set")}$destroy(){W(this,1),this.$destroy=b}$on(e,n){if(!Xe(n))return b;const l=this.$$.callbacks[e]||(this.$$.callbacks[e]=[]);return l.push(n),()=>{const i=l.indexOf(n);i!==-1&&l.splice(i,1)}}$set(e){this.$$set&&!ot(e)&&(this.$$.skip_bound=!0,this.$$set(e),this.$$.skip_bound=!1)}}const yt="4";typeof window<"u"&&(window.__svelte||(window.__svelte={v:new Set})).v.add(yt);function $t(t){let e;return{c(){e=g("section"),e.innerHTML=`<h3>Personalia</h3> <ul><li><span>📧</span> Email: klaas.post@windesheim.nl</li> <li><span>📞</span> Phone: +31 0640940856</li> <li><span>🏠</span> Address: Windesheim - T, 8017 CA Zwolle</li> <li><span>🔗</span> Links:
            <a href="https://github.com/KlaadPost">GitHub</a></li></ul>`},m(n,l){N(n,e,l)},p:b,i:b,o:b,d(n){n&&E(e)}}}class vt extends O{constructor(e){super(),R(this,e,null,$t,B,{})}}function kt(t){let e;return{c(){e=g("section"),e.innerHTML="<h3>Academic History</h3> <hr/> <hgroup><h5>Swaglord</h5> <p>Swag Academy, Sausland</p></hgroup> <hgroup><h5>HBO-ICT</h5> <p>Christelijke Hoge School Windesheim, Zwolle</p></hgroup>"},m(n,l){N(n,e,l)},p:b,i:b,o:b,d(n){n&&E(e)}}}class Et extends O{constructor(e){super(),R(this,e,null,kt,B,{})}}function Nt(t){let e;return{c(){e=g("section"),e.innerHTML='<img src="https://cdn.discordapp.com/attachments/890578750763331674/1161595408326332466/ProfilePicture.jpg" alt="Profile Picture" class="profile"/> <h1>Klaas Post</h1>',Se(e,"display","flex"),Se(e,"flex-direction","column"),Se(e,"align-items","center")},m(n,l){N(n,e,l)},p:b,i:b,o:b,d(n){n&&E(e)}}}class st extends O{constructor(e){super(),R(this,e,null,Nt,B,{})}}function Tt(t){let e,n,l,i,r,a;return e=new st({}),l=new Et({}),r=new vt({}),{c(){z(e.$$.fragment),n=S(),z(l.$$.fragment),i=S(),z(r.$$.fragment)},m(s,o){D(e,s,o),N(s,n,o),D(l,s,o),N(s,i,o),D(r,s,o),a=!0},p:b,i(s){a||(P(e.$$.fragment,s),P(l.$$.fragment,s),P(r.$$.fragment,s),a=!0)},o(s){j(e.$$.fragment,s),j(l.$$.fragment,s),j(r.$$.fragment,s),a=!1},d(s){s&&(E(n),E(i)),W(e,s),W(l,s),W(r,s)}}}class Ct extends O{constructor(e){super(),R(this,e,null,Tt,B,{})}}function St(t){let e;return{c(){e=g("section"),e.innerHTML='<h3>When am I available?</h3> <hr/> <figure class="grid"><p>mon 9-17</p> <p>tue 9-17</p> <p>wen 9-17</p> <p>thu 9-17</p> <p>fri 9-17</p></figure>'},m(n,l){N(n,e,l)},p:b,i:b,o:b,d(n){n&&E(e)}}}class Mt extends O{constructor(e){super(),R(this,e,null,St,B,{})}}const rt=async()=>{const t=document.querySelector('input[name="__RequestVerificationToken"]');if(t)return t.value;throw new Error("Token input element not found")},At=async()=>await grecaptcha.execute("6Lc_9TcpAAAAAIdlMq6r78wsWDrj6cELayKQWvw4",{action:"submit"});function Ve(t,e,n){const l=t.slice();return l[24]=e[n],l}function Ue(t){let e,n=t[24]+"",l;return{c(){e=g("p"),l=v(n),p(e,"class",t[7])},m(i,r){N(i,e,r),u(e,l)},p(i,r){r&64&&n!==(n=i[24]+"")&&X(l,n),r&128&&p(e,"class",i[7])},d(i){i&&E(e)}}}function Lt(t){let e,n,l,i,r,a,s,o,c,y,_=t[0].firstName?"• Enter a valid first name (min 2 letters)":"",f,d,h,K,J,H,Y,V,ne,Q=t[0].lastName?"• Enter a valid last name (min 2 letters)":"",Z,$,w,T,G,M,x,F,ce,se=t[0].email?"• Enter a valid email (name@example.com)":"",re,he,I,ge,je,fe,qe,pe,Be,_e=t[0].phone?"• Enter a valid phone number (10 digits)":"",Ee,Re,q,we,Oe,ee,be=t[5]?"Submitting...":"Submit",Ne,ye,He,$e,Te,Fe,ae=ke(t[6]),L=[];for(let m=0;m<ae.length;m+=1)L[m]=Ue(Ve(t,ae,m));return{c(){e=g("section"),n=g("h3"),n.textContent="Create a contact request",l=S(),i=g("hr"),r=S(),a=g("form"),s=g("label"),o=v(`First Name\r
            `),c=g("span"),y=v(" "),f=v(_),d=S(),h=g("input"),J=S(),H=g("label"),Y=v(`Last Name\r
            `),V=g("span"),ne=v(" "),Z=v(Q),$=S(),w=g("input"),G=S(),M=g("label"),x=v(`Email\r
            `),F=g("span"),ce=v(" "),re=v(se),he=S(),I=g("input"),je=S(),fe=g("label"),qe=v(`Phone\r
            `),pe=g("span"),Be=v(" "),Ee=v(_e),Re=S(),q=g("input"),Oe=S(),ee=g("button"),Ne=v(be),He=S(),$e=g("div");for(let m=0;m<L.length;m+=1)L[m].c();p(c,"class","secondary"),p(s,"for","firstName"),p(h,"type","text"),p(h,"id","firstName"),p(h,"aria-invalid",K=t[0].firstName),h.required=!0,p(h,"placeholder","name"),p(V,"class","secondary"),p(H,"for","lastName"),p(w,"type","text"),p(w,"id","lastName"),p(w,"aria-invalid",T=t[0].lastName),w.required=!0,p(w,"placeholder","surname"),p(F,"class","secondary"),p(M,"for","email"),p(I,"type","email"),p(I,"id","email"),p(I,"aria-invalid",ge=t[0].email),I.required=!0,p(I,"placeholder","name@example.com"),p(pe,"class","secondary"),p(fe,"for","phone"),p(q,"type","tel"),p(q,"id","phone"),p(q,"aria-invalid",we=t[0].phone),q.required=!0,p(q,"placeholder","0600000001"),p(ee,"type","submit"),p(ee,"aria-busy",t[5]),ee.disabled=ye=!t[8]},m(m,k){N(m,e,k),u(e,n),u(e,l),u(e,i),u(e,r),u(e,a),u(a,s),u(s,o),u(s,c),u(c,y),u(c,f),u(a,d),u(a,h),U(h,t[1]),u(a,J),u(a,H),u(H,Y),u(H,V),u(V,ne),u(V,Z),u(a,$),u(a,w),U(w,t[2]),u(a,G),u(a,M),u(M,x),u(M,F),u(F,ce),u(F,re),u(a,he),u(a,I),U(I,t[3]),u(a,je),u(a,fe),u(fe,qe),u(fe,pe),u(pe,Be),u(pe,Ee),u(a,Re),u(a,q),U(q,t[4]),u(a,Oe),u(a,ee),u(ee,Ne),u(a,He),u(a,$e);for(let C=0;C<L.length;C+=1)L[C]&&L[C].m($e,null);Te||(Fe=[A(h,"input",t[11]),A(h,"blur",t[12]),A(h,"input",t[13]),A(w,"input",t[14]),A(w,"blur",t[15]),A(w,"input",t[16]),A(I,"input",t[17]),A(I,"blur",t[18]),A(I,"input",t[19]),A(q,"input",t[20]),A(q,"blur",t[21]),A(q,"input",t[22]),A(a,"submit",xe(t[10]))],Te=!0)},p(m,[k]){if(k&1&&_!==(_=m[0].firstName?"• Enter a valid first name (min 2 letters)":"")&&X(f,_),k&1&&K!==(K=m[0].firstName)&&p(h,"aria-invalid",K),k&2&&h.value!==m[1]&&U(h,m[1]),k&1&&Q!==(Q=m[0].lastName?"• Enter a valid last name (min 2 letters)":"")&&X(Z,Q),k&1&&T!==(T=m[0].lastName)&&p(w,"aria-invalid",T),k&4&&w.value!==m[2]&&U(w,m[2]),k&1&&se!==(se=m[0].email?"• Enter a valid email (name@example.com)":"")&&X(re,se),k&1&&ge!==(ge=m[0].email)&&p(I,"aria-invalid",ge),k&8&&I.value!==m[3]&&U(I,m[3]),k&1&&_e!==(_e=m[0].phone?"• Enter a valid phone number (10 digits)":"")&&X(Ee,_e),k&1&&we!==(we=m[0].phone)&&p(q,"aria-invalid",we),k&16&&U(q,m[4]),k&32&&be!==(be=m[5]?"Submitting...":"Submit")&&X(Ne,be),k&32&&p(ee,"aria-busy",m[5]),k&256&&ye!==(ye=!m[8])&&(ee.disabled=ye),k&192){ae=ke(m[6]);let C;for(C=0;C<ae.length;C+=1){const De=Ve(m,ae,C);L[C]?L[C].p(De,k):(L[C]=Ue(De),L[C].c(),L[C].m($e,null))}for(;C<L.length;C+=1)L[C].d(1);L.length=ae.length}},i:b,o:b,d(m){m&&E(e),ut(L,m),Te=!1,te(Fe)}}}const Ae=/^.{2,1000}$/,ze=/^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/,Je=/^\d{10}$/;function Pt(t,e,n){let l,i="",r="",a="",s="",o=!1,c=null,y=[],_="",f={firstName:null,lastName:null,email:null,phone:null};const d=(M,x,F=Ae,ce=!0)=>{ce?n(0,f[M]=!F.test(x),f):n(0,f[M]=F.test(x)?!1:null,f)},h=async()=>{n(6,y=[]),n(5,o=!0);let M=!1;d("firstName",i),d("lastName",r),d("email",a),d("phone",s),l||(y.push("Invalid form data"),n(7,_="warning"),M=!0);const x=await rt();x||(y.push("Unable to get Anti-Forgery Token"),n(7,_="warning"),M=!0);const F=await At();if(F||(y.push("Unable to get Recaptcha Token"),n(7,_="warning"),M=!0),M)return;const se=JSON.stringify({FirstName:i,LastName:r,Email:a,PhoneNumber:s,RecaptchaToken:F});try{if(c=await fetch("https://localhost:44336/Contact",{method:"POST",headers:{"Content-Type":"application/json",RequestVerificationToken:x},body:se}),c.ok)y.push("Request has been sent"),n(7,_="success"),n(1,i=""),n(2,r=""),n(3,a=""),n(4,s=""),n(0,f.firstName=null,f),n(0,f.lastName=null,f),n(0,f.email=null,f),n(0,f.phone=null,f);else{const re=await c.json();n(6,y=Object.values(re).flatMap(he=>he)),n(7,_="warning")}}catch{y.push("Something went wrong while trying to send the email, try again later"),n(7,_="warning");return}finally{n(5,o=!1)}};function K(){i=this.value,n(1,i)}const J=()=>d("firstName",i),H=()=>d("firstName",i,Ae,!1);function Y(){r=this.value,n(2,r)}const V=()=>d("lastName",r),ne=()=>d("lastName",r,Ae,!1);function Q(){a=this.value,n(3,a)}const Z=()=>d("email",a,ze),$=()=>d("email",a,ze,!1);function w(){s=this.value,n(4,s)}const T=()=>d("phone",s,Je),G=()=>d("phone",s,Je,!1);return t.$$.update=()=>{t.$$.dirty&1&&n(8,l=!Object.values(f).some(M=>M===!0||M===null))},[f,i,r,a,s,o,y,_,l,d,h,K,J,H,Y,V,ne,Q,Z,$,w,T,G]}class It extends O{constructor(e){super(),R(this,e,Pt,Lt,B,{})}}function jt(t){let e,n,l,i,r,a;return e=new st({}),l=new Mt({}),r=new It({}),{c(){z(e.$$.fragment),n=S(),z(l.$$.fragment),i=S(),z(r.$$.fragment)},m(s,o){D(e,s,o),N(s,n,o),D(l,s,o),N(s,i,o),D(r,s,o),a=!0},p:b,i(s){a||(P(e.$$.fragment,s),P(l.$$.fragment,s),P(r.$$.fragment,s),a=!0)},o(s){j(e.$$.fragment,s),j(l.$$.fragment,s),j(r.$$.fragment,s),a=!1},d(s){s&&(E(n),E(i)),W(e,s),W(l,s),W(r,s)}}}class qt extends O{constructor(e){super(),R(this,e,null,jt,B,{})}}document.addEventListener("click",function(t){const e=document.getElementById("modal");e&&t.target===e&&e.close()});document.addEventListener("keydown",function(t){const e=document.getElementById("modal");e&&t.key==="Escape"&&e.close()});document.addEventListener("click",function(t){const e=document.getElementById("modal");e&&t.target instanceof HTMLAnchorElement&&t.target.getAttribute("aria-label")==="Close"&&t.target.parentElement===e&&e.close()});function Bt(t){let e,n=t[0].toLocaleString()+"",l;return{c(){e=g("small"),l=v(n),p(e,"class","secondary")},m(i,r){N(i,e,r),u(e,l)},p(i,[r]){r&1&&n!==(n=i[0].toLocaleString()+"")&&X(l,n)},i:b,o:b,d(i){i&&E(e)}}}function Rt(t,e,n){let{date:l}=e;return t.$$set=i=>{"date"in i&&n(0,l=i.date)},[l]}class Ot extends O{constructor(e){super(),R(this,e,Rt,Bt,B,{date:0})}}function Ht(t){let e;return{c(){e=g("p"),e.textContent="Loading..."},m(n,l){N(n,e,l)},p:b,i:b,o:b,d(n){n&&E(e)}}}function Ft(t){let e,n,l=t[0].senderName+"",i,r,a,s,o,c=t[0].message+"",y,_;return a=new Ot({props:{date:new Date(t[0].created)}}),{c(){e=g("hgroup"),n=g("h6"),i=v(l),r=v(" "),z(a.$$.fragment),s=S(),o=g("p"),y=v(c)},m(f,d){N(f,e,d),u(e,n),u(n,i),u(n,r),D(a,n,null),u(e,s),u(e,o),u(o,y),_=!0},p(f,d){(!_||d&1)&&l!==(l=f[0].senderName+"")&&X(i,l);const h={};d&1&&(h.date=new Date(f[0].created)),a.$set(h),(!_||d&1)&&c!==(c=f[0].message+"")&&X(y,c)},i(f){_||(P(a.$$.fragment,f),_=!0)},o(f){j(a.$$.fragment,f),_=!1},d(f){f&&E(e),W(a)}}}function Dt(t){let e,n,l,i;const r=[Ft,Ht],a=[];function s(o,c){return o[0]?0:1}return e=s(t),n=a[e]=r[e](t),{c(){n.c(),l=Ye()},m(o,c){a[e].m(o,c),N(o,l,c),i=!0},p(o,[c]){let y=e;e=s(o),e===y?a[e].p(o,c):(nt(),j(a[y],1,1,()=>{a[y]=null}),lt(),n=a[e],n?n.p(o,c):(n=a[e]=r[e](o),n.c()),P(n,1),n.m(l.parentNode,l))},i(o){i||(P(n),i=!0)},o(o){j(n),i=!1},d(o){o&&E(l),a[e].d(o)}}}function Wt(t,e,n){let{chatMessage:l}=e;return t.$$set=i=>{"chatMessage"in i&&n(0,l=i.chatMessage)},[l]}class Kt extends O{constructor(e){super(),R(this,e,Wt,Dt,B,{chatMessage:0})}}function Qe(t,e,n){const l=t.slice();return l[4]=e[n],l}function Ze(t,e){let n,l,i;return l=new Kt({props:{chatMessage:e[4]}}),{key:t,first:null,c(){n=Ye(),z(l.$$.fragment),this.first=n},m(r,a){N(r,n,a),D(l,r,a),i=!0},p(r,a){e=r;const s={};a&2&&(s.chatMessage=e[4]),l.$set(s)},i(r){i||(P(l.$$.fragment,r),i=!0)},o(r){j(l.$$.fragment,r),i=!1},d(r){r&&E(n),W(l,r)}}}function Vt(t){let e,n=[],l=new Map,i,r=ke(t[1]);const a=s=>s[4].id;for(let s=0;s<r.length;s+=1){let o=Qe(t,r,s),c=a(o);l.set(c,n[s]=Ze(c,o))}return{c(){e=g("article");for(let s=0;s<n.length;s+=1)n[s].c();p(e,"class","chatbox svelte-1pa7fcv")},m(s,o){N(s,e,o);for(let c=0;c<n.length;c+=1)n[c]&&n[c].m(e,null);t[2](e),i=!0},p(s,[o]){o&2&&(r=ke(s[1]),nt(),n=wt(n,o,a,1,s,r,l,e,_t,Ze,null,Qe),lt())},i(s){if(!i){for(let o=0;o<r.length;o+=1)P(n[o]);i=!0}},o(s){for(let o=0;o<n.length;o+=1)j(n[o]);i=!1},d(s){s&&E(e);for(let o=0;o<n.length;o+=1)n[o].d();t[2](null)}}}function Ut(t,e,n){let l,i=[];const r=()=>{l&&n(0,l.scrollTop=l.scrollHeight,l)};ft(async()=>{try{const s=await fetch("https://localhost:44336/Chat/Messages");s.ok?n(1,i=await s.json()):console.error(`Failed to fetch chat messages. Status: ${s.status}`)}catch(s){console.error("Error fetching chat messages:",s)}r()}),pt(r);function a(s){Le[s?"unshift":"push"](()=>{l=s,n(0,l)})}return[l,i,a]}class zt extends O{constructor(e){super(),R(this,e,Ut,Vt,B,{})}}function Jt(t){let e,n,l,i;return{c(){e=g("form"),n=g("textarea"),p(n,"maxlength","1000"),p(n,"class","message-input")},m(r,a){N(r,e,a),u(e,n),U(n,t[0]),l||(i=[A(n,"input",t[3]),A(n,"keyup",t[2]),A(e,"submit",xe(t[1]))],l=!0)},p(r,[a]){a&1&&U(n,r[0])},i:b,o:b,d(r){r&&E(e),l=!1,te(i)}}}function Qt(t,e,n){let l="";const i=async()=>{const s=JSON.stringify({Message:l}),o=await rt();l.trim()!==""&&(await fetch("https://localhost:44336/Chat",{method:"POST",headers:{"Content-Type":"application/json",RequestVerificationToken:o},body:s}),n(0,l=""))},r=s=>{s.key==="Enter"&&!s.shiftKey&&(s.preventDefault(),i())};function a(){l=this.value,n(0,l)}return[l,i,r,a]}class Zt extends O{constructor(e){super(),R(this,e,Qt,Jt,B,{})}}function Gt(t){let e,n,l,i;return e=new zt({}),l=new Zt({}),{c(){z(e.$$.fragment),n=S(),z(l.$$.fragment)},m(r,a){D(e,r,a),N(r,n,a),D(l,r,a),i=!0},p:b,i(r){i||(P(e.$$.fragment,r),P(l.$$.fragment,r),i=!0)},o(r){j(e.$$.fragment,r),j(l.$$.fragment,r),i=!1},d(r){r&&E(n),W(e,r),W(l,r)}}}class Xt extends O{constructor(e){super(),R(this,e,null,Gt,B,{})}}function Yt(){const t=document.getElementById("profile"),e=document.getElementById("contact"),n=document.getElementById("chat");t&&new Ct({target:t}),e&&new qt({target:e}),n&&new Xt({target:n})}function xt(){const t=document.createElement("script");t.src="https://www.google.com/recaptcha/api.js?render=6Lc_9TcpAAAAAIdlMq6r78wsWDrj6cELayKQWvw4",t.async=!0,t.defer=!0,document.head.appendChild(t)}xt();Yt();
