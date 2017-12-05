(handler "COLORPUSH"
  (lambda (data)
    (let* ((hids (take 124 (hids: data)))
          (colors (take 124 (colors: data))))
      (on-device "rgb-per-key-zones" show-on-keys: hids colors))))

(add-event-per-key-zone-use "COLORPUSH" "all")